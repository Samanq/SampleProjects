using FluentResultSample.Application.Common.Errors;
using FluentResultSample.Application.Dtos;
using FluentResultSample.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FluentResultSample.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IStudentRepository _studentRepository;

    // Injecting the IStudentRepository
    public StudentsController(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    [HttpGet]
    public IActionResult GetById(int id)
    {
        return Ok(_studentRepository.GetById(id));
    }

    [HttpPost]
    public IActionResult Create(CreateStudentDto request)
    {
        var result = _studentRepository.Create(request);

        if (result.IsSuccess)
        {
            return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = result.Value.Id },
            value: result);
        }

        // If result is not succes getting the first error and return a problem,
        var firstError = result.Errors[0];
        if (firstError is DuplicateEmailError)
        {
            return Problem(statusCode: 409, detail: firstError.Message);
        }

        return Problem();
    }
}
