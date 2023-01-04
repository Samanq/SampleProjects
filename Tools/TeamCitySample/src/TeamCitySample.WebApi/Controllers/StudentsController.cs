using Microsoft.AspNetCore.Mvc;

namespace TeamCitySample.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private List<Student> _students = new List<Student>
    {
        new Student{Name= "John", Age = 20},
        new Student{Name= "jane", Age = 21},
        new Student{Name= "Peter", Age = 22}
    };

    [HttpGet]
    public IActionResult GetS()
    {

        return Ok(_students);
    }
}
