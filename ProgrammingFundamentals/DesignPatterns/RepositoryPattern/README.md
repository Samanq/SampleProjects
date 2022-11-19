# Repository Pattern
**Microsoft**: "Repositories are classes or components that encapsulate the logic required to access data sources."

**Martin Fowler**: "repositiry Mediates between the domain and data mapping layers using a collection-like interface for accessing domain objects." 

---
## Defining Entities in Domain layer
Create a class named **Person** in **Domain/Entities**
```C#
// Our Entities can be in Domain layer
namespace RepositoryPattern.Domain.Entities;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
```
## Creating the Repository interface in Application layer
Create an interface named **IPersonRepository** in **Application/Common/Interfaces/Persistence**
```C#
// Our Repository Interface can be in Application layer.
namespace RepositoryPattern.Application.Common.Interfaces.Persistence;
using RepositoryPattern.Domain.Entities;

public interface IPersonRepository
{
    void AddUser(Person person);

    Person? GetPersonById(int id);

    Person? GetPersonByEmail(string email);

    IEnumerable<Person>? GetPeople();

    void Delete(Person person);
}
```

## Implementing the repository Interface in Infrastructure layer
Create a class named PersonRepository in **Infrastructure/Persistence/Repositories** and implement the IPersonRepository.
```C#
// Impementation of Repository can be in Infrastructure layer.
namespace RepositoryPattern.Infrastructure.Persistence.Repositories;

using RepositoryPattern.Application.Common.Interfaces.Persistence;
using RepositoryPattern.Domain.Entities;

public class PersonRepository : IPersonRepository
{
    private static List<Person> _persons = new List<Person>();

    public void AddUser(Person person)
    {
        _persons.Add(person);
    }

    public void Delete(Person person)
    {
        _persons.Remove(person);
    }

    public IEnumerable<Person>? GetPeople()
    {
        return _persons;
    }

    public Person? GetPersonByEmail(string email)
    {
        return _persons.Where(p => p.Email.ToLower() == email.ToLower())
            .FirstOrDefault();
    }

    public Person? GetPersonById(int id)
    {
        return _persons.Where(p => p.Id == id)
            .FirstOrDefault();
    }
}

```

## Creating the controller in Presentation layer
Create a API Controller in controllers folder and Inject the IPersonRepository
```C#
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

```

## Registering the IPersonRepository
Register the IPersonRepository in **program.cs** ass a scoped.
```C#
// Registering PersonRepository
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
```