
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductRepository repository) : ControllerBase
{
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return Ok(await repository.GetAllAsync());
    }

    [HttpGet("{id:int}")]   // api/products/2
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product == null) 
            return NotFound();
        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync();
        return product;
    }

    [HttpPut("{id:int}")]   
    public async Task<ActionResult> UpdateProduct(int id, [FromBody] Product product)
    {
        if (id != product.Id || !ProductExists(id))
            return BadRequest("Cannot update product");
        context.Entry(product).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product == null)
            return NotFound();
        context.Products.Remove(product);
        await context.SaveChangesAsync();
        return NoContent();
    }

    private bool ProductExists(int id)
    {
        return context.Products.Any(e => e.Id == id);
    }
}