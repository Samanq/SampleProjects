# FluentResult

## Application layer
1. Install **FluentResults** packge

2. Create an interface for repository
```C#
namespace FluentResultSample.Application.Repositories;

public interface IStudentRepository
{
    Student? GetById(int id);
    Student? GetByEmail(string email);

    // It returns s student or list of errors
    Result<Student> Create(CreateStudentDto dto);
}
``` 

3. In **Common/Errors** folder Create a class for **DuplicatedEmailError** and implement the **IError**
```C#
namespace FluentResultSample.Application.Common.Errors;

public class DuplicateEmailError : IError
{
    public List<IError> Reasons => throw new NotImplementedException();

    public string Message => "Email already exist";

    public Dictionary<string, object> Metadata => throw new NotImplementedException();
}
```
---
## Infrastructure layer
1. Create a class for **StudentRepository** and implement the **IStudentRepository** then you can return a student or a Result.
```C#
namespace FluentResultSample.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private static List<Student> _students = new List<Student>
    {
        new Student
        {
            Id = 1,
            Name = "Test",
            Email = "john@example.com",
            Age= 30,
        }
    };

    public Student? GetById(int id)
    {
        return _students
          .Where(s => s.Id == id)
          .SingleOrDefault();
    }
    public Student? GetByEmail(string email)
    {
        return _students
          .Where(s => s.Email.ToLower() == email.ToLower())
          .SingleOrDefault();
    }

    public Result<Student> Create(CreateStudentDto dto)
    {
        // If user exists return an Error otherwise create a new user and return the user.
        if (GetByEmail(dto.Email) is not null)
        {
            // Return an error result
            return Result.Fail<Student>(new[] { new DuplicateEmailError() });
        }

        var student = new Student
        {
            Id = 0,
            Name = dto.Name,
            Email = dto.Email,
            Age = dto.Age,
        };

        _students.Add(student);

        return student;
    }
}
```
---
## API layer
1. Create a **ErrorController** for handling the errors
```C#
namespace FluentResultSample.Api.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}
```
2. Create a controller for Students and inject the **IStudentRepository**
```C#
[Route("[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IStudentRepository _studentRepository;

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
```