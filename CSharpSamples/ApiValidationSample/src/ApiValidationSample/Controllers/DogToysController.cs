using ApiValidationSample.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiValidationSample.Controllers;

[Route("[controller]")]
[ApiController]
public class DogToysController : ControllerBase
{
    private readonly IDogToyService _dogToyService;

    public DogToysController(IDogToyService dogToyService)
    {
        _dogToyService = dogToyService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_dogToyService.GetAll());
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name, string color)
    {
        // Create method inside the DogToyService is responsible for validation
        await _dogToyService.Create(new Models.DogToy
        { 
            Name = name,
            Color = color,
            HasSound = true 
        });

        return Ok(_dogToyService.GetAll());
    }
}
