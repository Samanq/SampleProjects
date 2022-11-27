using MediatR;
using MediatRWithValidationPipeline.Application.Features.Students.Commands.CreateStudent;
using MediatRWithValidationPipeline.Application.Features.Students.Queries.GetStudentById;
using Microsoft.AspNetCore.Mvc;

namespace MediatRWithValidationPipeline.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IMediator _mediator;

    // Injecting MediatR
    public StudentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) 
    {
        var result = await _mediator.Send(new GetStudentByIdQuery(id));

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStudentCommand command)
    {
        var result = await _mediator.Send(command);

        if (result is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = result.Id },
            value: result);
    }
}
