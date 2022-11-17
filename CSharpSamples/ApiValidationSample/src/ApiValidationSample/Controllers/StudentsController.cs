using ApiValidationSample.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiValidationSample.Controllers;

[Route("[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private static List<Student> _students = new List<Student> 
    {
        new Student
        {
            FirstName= "John",
            LastName= "Doe",
            Code= "abc",
            Email= "John.Doe@Exampl.com",
            PhoneNumber = "113"
        }
    };


    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_students);
    }

    [HttpGet("GetByLastName")]
    public IActionResult GetByLastName([StringLength(10)] string LastName)
    {
        return Ok(_students
            .Where(s => s.LastName.ToLower() == LastName.ToLower()));
    }

    [HttpPost]
    public IActionResult Create([FromBody] Student student)
    {
        _students.Add(student);

        return Ok(_students);
    }
}
