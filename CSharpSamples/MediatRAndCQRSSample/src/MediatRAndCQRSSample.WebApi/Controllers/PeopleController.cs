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
