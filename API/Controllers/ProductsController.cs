
using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController(IGenericRepository<Product> repository) : BaseApiController
{
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecificationParams specificationParams)
    {
        var spec = new ProductSpecification(specificationParams);

        return await CreatePagedResult(repository, spec, specificationParams.PageIndex, specificationParams.PageSize);
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
        if (await repository.SaveAllAsync())
            return CreatedAtAction("GetProduct", new { id =  product.Id }, product);
        return BadRequest("Problem in product creation");
    }

    [HttpPut("{id:int}")]   
    public async Task<ActionResult> UpdateProduct(int id, [FromBody] Product product)
    {
        if (id != product.Id || !ProductExists(id))
            return BadRequest("Cannot update product");
        repository.Update(product);       
        if (await repository.SaveAllAsync())
            return NoContent();
        return BadRequest("Problem in product updating");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repository.GetByIdAsync(id);
        if (product == null)
            return NotFound();
        repository.Remove(product);
        if (await repository.SaveAllAsync())
            return NoContent();
        return BadRequest("Problem in product deletion");
    }


    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var spec = new BrandListSpecification();
        var result = await repository.ListAsync(spec);
        return Ok(result);
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var spec = new TypeListSpecification();
        var result = await repository.ListAsync(spec);
        return Ok(result);
    }

    private bool ProductExists(int id)
    {
        return repository.IsExists(id);
    }
}