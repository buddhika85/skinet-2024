using System.Text.Json;
using API.Middleware;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

// DB connection string read from AppSettings.json
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Scoped only to HTTP Request
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// CORS support
// CORS configuration below
builder.Services.AddCors();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Exception handling in middleware level
app.UseMiddleware<ExceptionMiddleware>();

// CORS configuration
// The API methods can only be accessed from below URL clients
// Other clients cannot access it
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
                    .WithOrigins("http://localhost:4200", "https://localhost:4200"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.MapControllers();

// seeding
try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StoreContext>();
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
    //await context.SaveChangesAsync();
}
catch(Exception ex)
{
    Console.WriteLine(ex);
    throw;
}

app.Run();