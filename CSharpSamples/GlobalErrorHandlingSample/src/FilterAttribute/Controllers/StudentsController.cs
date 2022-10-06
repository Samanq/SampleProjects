using FilterAttribute.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilterAttribute.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private static List<Student> students = new List<Student>
    {
        new Student{Id = 1, Name = "John", Email = "John@Example.com"},
        new Student{Id = 2, Name = "Jane", Email = "Jane@Example.com"},
        new Student{Id = 3, Name = "Peter", Email = "Peter@Example.com"}
    };

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(students);
    }

    [HttpGet("{id}")]
    public IActionResult Get(long id)
    {
        return Ok(students.SingleOrDefault(s => s.Id == id));
    }

    [HttpPost]
    public IActionResult Create(Student student)
    {
        var currentStudent = students.SingleOrDefault(s => s.Email.ToLower() == student.Email.ToLower());

        if (currentStudent is not null)
        {
            throw new Exception("Email already exist");
        }

        students.Add(student);

        return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
    }
}
