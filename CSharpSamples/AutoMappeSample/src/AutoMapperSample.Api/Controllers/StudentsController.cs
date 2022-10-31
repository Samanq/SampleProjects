namespace AutoMapperSample.Api.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IMapper _mapper;

    // Injecting AutoMapper
    public StudentsController(IMapper mapper) => _mapper = mapper;
    

    [HttpGet]
    public ActionResult<List<StudentDto>> GetStudents()
    {
        return Ok(GenerateStudents().Select(student => _mapper.Map<StudentDto>(student)));
    }


    private IEnumerable<Student> GenerateStudents()
    {
        var students = new List<Student> 
        {
            new Student
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Age = 20,
            },
            new Student
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Doe",
                Age = 19,
            },
            new Student
            {
                Id = 3,
                FirstName = "Peter",
                LastName = "Jackson",
                Age = 39,
            }
        };

        return students;
    }
}
