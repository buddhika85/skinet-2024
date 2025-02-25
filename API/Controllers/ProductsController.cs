
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductRepository repository) : ControllerBase
{
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
    {
        return Ok(await repository.GetAllAsync());
    }

    [HttpGet("{id:int}")]   // api/products/2
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repository.GetByIdAsync(id);
        if (product == null) 
            return NotFound();
        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
    {
        repository.Add(product);
        if (await repository.SaveChangesAsync())
            return CreatedAtAction("GetProduct", new { id =  product.Id }, product);
        return BadRequest("Problem in product creation");
    }

    [HttpPut("{id:int}")]   
    public async Task<ActionResult> UpdateProduct(int id, [FromBody] Product product)
    {
        if (id != product.Id || !ProductExists(id))
            return BadRequest("Cannot update product");
        repository.Update(product);       
        if (await repository.SaveChangesAsync())
            return NoContent();
        return BadRequest("Problem in product updating");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repository.GetByIdAsync(id);
        if (product == null)
            return NotFound();
        repository.Delete(product);
        if (await repository.SaveChangesAsync())
            return NoContent();
        return BadRequest("Problem in product deletion");
    }

    private bool ProductExists(int id)
    {
        return repository.IsExists(id);
    }
}