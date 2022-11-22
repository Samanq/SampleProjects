using ApiValidationSample.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ApiValidationSample.Controllers;

[Route("[controller]")]
[ApiController]
public class DogsController : ControllerBase
{
    private static List<Dog> _dogs = new List<Dog>
    {
        new Dog
        {
            Age= 1,
            Color = "Golden color",
            Name = "Pico",
            OwnerEmail = "Pico@Example.com"
        }
    };   


    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_dogs);
    }

    [HttpPost]
    public IActionResult Create([FromBody]Dog dog)
    {
        _dogs.Add(dog);

        return Ok(_dogs);
    }
}
