using Core.Entities;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        // DROP DB - dotnet ef database drop -p Infrastructure -s API
        // 
        public static async Task SeedAsync(StoreContext context)
        {
            if (! context.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products == null)
                    return;
                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }
        }
    }
}
