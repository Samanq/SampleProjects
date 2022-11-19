using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Application.Common.Interfaces.Persistence;
using RepositoryPattern.Domain.Entities;
using RepositoryPattern.Dtos;

namespace RepositoryPattern.Controllers;

[Route("[controller]")]
[ApiController]
public class PeopleController : ControllerBase
{
    private readonly IPersonRepository _repository;

    // Injecting PersonRepository from Application layer into the controller in presentation layer
    public PeopleController(IPersonRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        return Ok(_repository.GetPeople());
    }

    [HttpGet("GetById/{id}")]
    public IActionResult GetById(int id)
    {
        return Ok(_repository.GetPersonById(id));
    }

    [HttpGet("GetByEmail/{email}")]
    public IActionResult GetById(string email)
    {
        return Ok(_repository.GetPersonByEmail(email));
    }

    [HttpPost("Create")]
    public IActionResult Create([FromBody] PersonRequest request)
    {
        var person = new Person
        {
            Id = request.Id,
            Name = request.Name,
            Email = request.Email,
        };

        _repository.AddUser(person);

        return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
    }
}
