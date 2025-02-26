
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductRepository repository) : ControllerBase
{
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type)
    {
        return Ok(await repository.GetAllAsync(brand, type));
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


    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        return Ok(await repository.GetBrandsAsync());
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        return Ok(await repository.GetTypesAsync());
    }

    private bool ProductExists(int id)
    {
        return repository.IsExists(id);
    }
}