using Microsoft.AspNetCore.Mvc;
using Practica_CSR.Models.DTOs.Reponses;
using Practica_CSR.Models.DTOs.Requests;
using Practica_CSR.Services.Implementations;
using Practica_CSR.Services.Interfaces;

namespace Practica_CSR.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAllProducts());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        ProductForReadDto? product = _service.GetProductById(id);

        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }
    [HttpPost]
    public IActionResult Create(ProductForCreateDto data)
    {
        if (_service.NameExists(data.Name))
        {
            return Conflict("Ya existe un producto con ese nombre.");
        }
        
        ProductForReadDto created = _service.CreateProduct(data);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    [HttpPut("{id}")]
    public IActionResult Update(int id, ProductForUpdateDto data)
    {
        if (_service.GetProductById(id) == null)
        {
            return NotFound();
        }

        _service.UpdateProduct(id, data);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (_service.GetProductById(id) == null)
        {
            return NotFound();
        }

        _service.DeleteProduct(id);
        return NoContent();
    }
    [HttpGet("search")]
    public IActionResult Search(string name)
    {
        return Ok(_service.SearchProductsByName(name));
    }

    [HttpGet("stats")]
    public IActionResult GetStats()
    {
        ProductStatsDto stats = _service.GetStats();
        return Ok(stats);
    }
}

