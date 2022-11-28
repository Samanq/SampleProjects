using FluentResults;
using FluentResultSample.Application.Common.Errors;
using FluentResultSample.Application.Dtos;
using FluentResultSample.Application.Repositories;
using FluentResultSample.Domain.Entities;

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
