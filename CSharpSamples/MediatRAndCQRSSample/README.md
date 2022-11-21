# MediatR and CQRS Sample

## Projects
1. Create a WebApi project named **MediatRAndCQRSSample.WebApi**

2. Create a class library project **MediatRAndCQRSSample.Application**

3. Create a class library project **MediatRAndCQRSSample.Domain**

3. Create a class library project **MediatRAndCQRSSample.Infrastructure**

## References
1. In MediatRAndCQRSSample.**Application** project add a reference to MediatRAndCQRSSample.**Domain** .

2. In MediatRAndCQRSSample.**WebApi** project add a reference to MediatRAndCQRSSample.**Application** .

3. In MediatRAndCQRSSample.**Infrastructure** project add a reference to MediatRAndCQRSSample.**Application** .

## Packges
In the WebAPI project install **MediatR** package from nuget.


## Domain Layer
1. Create a folder named **Entities** and create class named Person
```C#
namespace MediatRAndCQRSSample.Domain.Entities;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

```

## Application Layer

### Installing required packages
1. Install **MediatR.Extensions.Microsoft.DependencyInjection** package
2. Install **Microsoft.Extensions.DependencyInjection.Abstractions** package

### Defining Repository interface
1. Create a folder named **Repositories**, then create an interface called **IPersonRepository**
```C#
using MediatRAndCQRSSample.Domain.Entities;

namespace MediatRAndCQRSSample.Application.Repositories;

public interface IPesonRepository
{
    IEnumerable<Person>? GetAll();

    Person? GetById(int id);

    Person? GetByEmail(string email);

    Person Create(Person person);
}
```

### Defining Commands and queries for MediatR
A **command** does something but does not return a value

A **Query** returns a result
1. Create folder named **Features**, then create a folder inside that named **People** for your entity.

2. Inside **People** create two folder named **Commands** and **Queries**.

3. Inside **Queries** folder create a folder named **GetAllPeople** and inside that create a **record** called **GetAllPeopleQuery** and Inherite from **IRequest** and pass the return type.
```C#
using MediatR;
using MediatRAndCQRSSample.Domain.Entities;

namespace MediatRAndCQRSSample.Application.Features.People.Queries.GetAllPeople;

// The return type is: IEnumerable<Person>
public record GetAllPeopleQuery() : IRequest<IEnumerable<Person>>;
```

4. Inside **GetAllPeople** folder create a class named **GetAllPeopleHandler** and implement **IRequestHandler** and pass the query and return type. Afterward inject the **IRepository** for accessing the data. In the **Handle** method you can define your codes.
```C#
using MediatR;
using MediatRAndCQRSSample.Application.Repositories;
using MediatRAndCQRSSample.Domain.Entities;

namespace MediatRAndCQRSSample.Application.Features.People.Queries.GetAllPeople;

// The query for this handler is: GetAllPeopleQuery
// The return type is: IEnumerable<Person>
public class GetAllPeopleHandler : IRequestHandler<GetAllPeopleQuery, IEnumerable<Person>>
{
    private readonly IPesonRepository _personRepository;

    // Injecting IPersonRepository
    public GetAllPeopleHandler(IPesonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public Task<IEnumerable<Person>> Handle(GetAllPeopleQuery request, CancellationToken cancellationToken)
    {
        // Getting the data from repository
        return Task.FromResult(_personRepository.GetAll());
    }
}

```
5. You can follow the same principles for the commands.

6. In the root of the **MediatRAndCQRSSample.Application** project create a class named **DependencyInjection** and register the MediatR.
```C#
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MediatRAndCQRSSample.Application;

public static class DependencyInjection
{
    // Registering the application services
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        return services;
    }
}
```

## Infrastructure Layer
### Installing required packages
1. Install **Microsoft.Extensions.DependencyInjection.Abstractions** package

2. Create a folder named **Persistence**, inside that create a folder named **Repositories** and inside that create a class named **PersonRepository** and implement the **IPersonRepository** interface that we defined in application layer.
```C#
using MediatRAndCQRSSample.Application.Repositories;
using MediatRAndCQRSSample.Domain.Entities;

namespace MediatRAndCQRSSample.Infrastructure.Persistence.Repositories;

public class PersonRepository : IPesonRepository
{
    private static List<Person> _people = new List<Person> 
    {
        new Person
        {
            Id = 1,
            Name = "John",
            Email = "John@Example.com",
        },
        new Person 
        {
            Id = 2,
            Name = "Jane",
            Email = "Jane@Example.com",
        }
    };

    public Person Create(Person person)
    {
        _people.Add(person);
        return person;
    }

    public IEnumerable<Person>? GetAll()
    {
        return _people;
    }

    public Person? GetByEmail(string email)
    {
        return _people
            .Where(p => p.Email.ToLower() == email.ToLower())
            .SingleOrDefault();
    }

    public Person? GetById(int id)
    {
        return _people
            .Where(p => p.Id == id)
            .SingleOrDefault();
    }
}
```

3. In the root of the **MediatRAndCQRSSample.Infrastructure** project create a class named **DependencyInjection** and register the Repository. 
```C#
using MediatRAndCQRSSample.Application.Repositories;
using MediatRAndCQRSSample.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MediatRAndCQRSSample.Infrastructure;

public static class DependencyInjection
{
    // Registering Infrastructure services
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IPesonRepository, PersonRepository>();
        return services;
    }
}

```


## Presentation Layer (API)

1. In **Program.cs** Add Infrastructure and application dependecies
```C#
builder.Services
    .AddInfrastructure() // Registering Infrastrucre services
    .AddApplication();  // Registering application services
```

2. Create an **ApiController** called PeopleController and inject the **IMediator**
```C#
using MediatR;
using MediatRAndCQRSSample.Application.Features.People.Commands.CreatePerson;
using MediatRAndCQRSSample.Application.Features.People.Queries.GetAllPeople;
using MediatRAndCQRSSample.Application.Features.People.Queries.GetPersonById;
using Microsoft.AspNetCore.Mvc;

namespace MediatRAndCQRSSample.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class PeopleController : ControllerBase
{
    private readonly IMediator _mediator;

    // Injecting IMediator
    public PeopleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllPeopleQuery());

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetPersonByIdQuery(id));

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(int id, string name, string email)
    {
        // Creating the command
        var command = new CreatePersonCommand(id, name, email);

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new {id = result.Id }, result);
    }
}

```