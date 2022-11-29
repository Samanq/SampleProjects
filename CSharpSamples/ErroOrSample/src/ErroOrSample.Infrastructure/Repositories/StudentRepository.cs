using ErroOrSample.Application.Repositories;
using ErroOrSample.Domian.Common.Errors;
using ErroOrSample.Domian.Entities;
using ErrorOr;

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
