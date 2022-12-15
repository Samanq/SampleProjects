using ErroOrSample.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ErroOrSample.Api.Controllers;


public class StudentsController : ApiController
{
    private readonly IStudentRepository _studentRepository;

    public StudentsController(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }


    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok(_studentRepository.GetById(id));
    }

    [HttpGet("GetByEmail")]
    public IActionResult GetByEmail(string email)
    {
        return Ok(_studentRepository.GetByEmail(email));
    }

    [HttpPost]
    public IActionResult Create(string name, string email, int age)
    {
        var result = _studentRepository.Create(name, email, age);

        return result.Match(
            res => Ok(res),
            errors => Problem(errors));
    }
}
