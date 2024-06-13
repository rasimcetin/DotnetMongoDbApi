using DotnetMongoDbApi.Model;
using DotnetMongoDbApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotnetMongoDbApi;

[ApiController]
[Route("api/[controller]")]
public class ProductController(ProcuctService procuctService) :ControllerBase
{

    [HttpGet]
    public async Task<List<Product>> Get()
    {
        return await procuctService.GetAsync();
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Product>> Get(string id){
        var product = await procuctService.GetAsync(id);
        if(product is null){
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product newProduct){
        await procuctService.CreateAsync(newProduct);
        return CreatedAtAction(nameof(Get), new {id = newProduct.Id}, newProduct);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Product updatedProduct){
        var product = await procuctService.GetAsync(id);
        if(product is null){
            return NotFound();
        }

        await procuctService.UpdateAsync(id, updatedProduct);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id){
        var product = await procuctService.GetAsync(id);
        if(product is null){
            return NotFound();
        }

        await procuctService.RemoveAsync(id);
        return NoContent();
    }
}
