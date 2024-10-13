using Microsoft.AspNetCore.Mvc;

namespace OpenTelemetrySample.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    //private readonly ILogger<StudentsController> _logger;

    [HttpGet(Name = "GetStudents")]
    public IEnumerable<Student> Get()
    {
        List<Student> students = [
            new Student(1, "John", 34),
            new Student(1, "Jane", 35)
        ];

        return students;
    }
}

public record Student(long Id, string Name, int Age);
