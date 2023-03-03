using AuditTrailSample.Application.Repositories;
using AuditTrailSample.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuditTrailSample.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await _productRepository.GetById(id);

        return Ok(result);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productRepository.GetAll();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        product.CreatedDate = DateTime.UtcNow;
        product.ModifiedDate = DateTime.UtcNow;

        await _productRepository.Create(product);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(long id, Product product)
    {
        var currectProduct = await _productRepository.GetById(id);
        if (currectProduct == null) 
        {
            return NotFound();
        }

        currectProduct.ModifiedDate = DateTime.UtcNow;
        currectProduct.Price = product.Price;
        currectProduct.Title = product.Title;

        await _productRepository.Edit(currectProduct);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(long id)
    {
        var product = await _productRepository.GetById(id);
        if (product == null)
        {
            return NotFound();
        }

        await _productRepository.Delete(product);

        return Ok();
    }
}
