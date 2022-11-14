using Microsoft.AspNetCore.Mvc;

namespace SerilogSample.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private static List<Student> students = new List<Student>
        {
            new Student
            {
                Age= 20,
                Name = "John"
            },
            new Student
            {
                Age= 19,
                Name = "Jane"
            },
            new Student
            {
                Age= 25,
                Name = "Peter"
            },
        };

        private readonly ILogger<StudentsController> _logger;

        public StudentsController(ILogger<StudentsController> logger)
        {
            _logger = logger;
        }


        [HttpGet("GenerateError")]
        public IActionResult GenerateError()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i == 8)
                {
                    _logger.LogError("{counter} is not valid", i);
                }
                else
                {
                    _logger.LogInformation("{counter} is approved", i);
                }
            }
            return Ok();
        }

        [HttpGet("ThrowException")]
        public IActionResult ThrowException()
        {
            try
            {
                students.Add(new Student { Name = "James", Age = int.Parse("abc") });
                _logger.LogInformation("new student created");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
        }
    }
}

public class Student
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
}