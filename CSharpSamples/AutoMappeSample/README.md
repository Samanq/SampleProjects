# AutoMapper Sample
Using Web API with .Net 6

---


## Steps
1. Create WebAPI project.
2. Install these packages
    - AutoMapper.Extensions.Microsoft.DependencyInjection
3. In Program.cs add Automapper service
```C#
// Adding AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);
```

4. Create Student and studentDto classes for mapping sample.
```c#
namespace AutoMapperSample.Api;

public class StudentDto
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
}
```
```c#
namespace AutoMapperSample.Api;

public class Student
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
}

```
5. Create a Api Controller for students and inject the AutoMapper,
```C#
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
}
```

6. Create a class named AutoMapperProfile.cs and inherite from Profile for defining AutoMapper profile. 

    In the constructor Create the maps you need.
```C#
namespace AutoMapperSample.Api;
using AutoMapper;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
		// For Simple Mapping
		//CreateMap<Student, StudentDto>();

		// When source and destination have diffrent properties
        CreateMap<Student, StudentDto>()
			.ForMember(dest =>
			dest.FName,
			opt => opt.MapFrom(src => src.FirstName))
			.ForMember(dest =>
			dest.LName,
			opt => opt.MapFrom(src => src.LastName));

        CreateMap<StudentDto, Student>()
			.ForMember(dest => 
			dest.FirstName,
			opt => opt.MapFrom(src => src.FName))
            .ForMember(dest =>
            dest.LastName,
            opt => opt.MapFrom(src => src.LName));
    }
}

```
7. In the StudentsController you can map your objects like this
```C#
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
```