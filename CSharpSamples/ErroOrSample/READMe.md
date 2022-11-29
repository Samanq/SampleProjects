# ErrorOr

## Domain Layer
1. Install **ErrorOr** Package.

2. Create a class named **Student** in **Entities** folder.
```C#
namespace ErroOrSample.Domian.Entities;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Age { get; set; }
}
```

3. Create class **Errors** in inside that create another class named **Students** in **Common/Errors** folder
```C#
namespace ErroOrSample.Domian.Common.Errors;

public static class Errors
{
    public static class Students
    {
        public static Error DuplicatedEmail => Error.Conflict(
            code: "Student.DuplicateEmail",
            description: "Email already exist.");
    }
}

```
---

## Application Layer
1. Create the **IStudentRepository** interface.
```C#
namespace ErroOrSample.Application.Repositories;

public interface IStudentRepository
{
    Student? GetById(int id);
    Student? GetByEmail(string email);

    // Return a student or error
    ErrorOr<Student> Create(string name, string email, int age);
}

```
---

## Infrastructure Layer
1. Implement the **IStudentRepository**
```C#
namespace ErroOrSample.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private static List<Student> _students = new List<Student>
    {
        new Student
        {
            Id = 1,
            Name = "John",
            Email = "John@Example.com",
            Age = 20
        }
    };

    public ErrorOr<Student> Create(string name, string email, int age)
    {
        if (GetByEmail(email) is not null)
        {
            // Return an error from domain layer
            return Errors.Students.DuplicatedEmail;
        }

        var student = new Student
        {
            Id = 2,
            Name = name,
            Email = email,
            Age = age
        };

        _students.Add(student);

        return student;
    }

    public Student? GetByEmail(string email)
    {
        return _students
            .Where(s => s.Email.ToLower() == email.ToLower())
            .SingleOrDefault();
    }

    public Student? GetById(int id)
    {
        return _students
            .Where(s => s.Id == id)
            .SingleOrDefault();
    }
}
```
---

## API Layer
1. Create a Api controller and name id ApiController, then, create an ActionResult and name it **Problem**.
```C#
namespace ErroOrSample.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        var firstError = errors[0];

        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(
            statusCode: statusCode,
            title: firstError.Description);
    }
}
```
2. Create a api controller for students and inherite from **ApiController**.
```C#
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
    public IActionResult GetByEmail(string name, string email, int age)
    {
        var result = _studentRepository.Create(name, email, age);

        return result.Match(
            res => Ok(res),
            errors => Problem(errors));
    }
}
```