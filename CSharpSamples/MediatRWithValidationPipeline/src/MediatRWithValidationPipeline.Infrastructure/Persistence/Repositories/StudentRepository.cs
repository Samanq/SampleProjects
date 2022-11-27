using MediatRWithValidationPipeline.Application.Repositories;
using MediatRWithValidationPipeline.Domain.Entities;

namespace MediatRWithValidationPipeline.Infrastructure.Persistence.Repositories;

public class StudentRepository : IStudentRepository
{
    private static List<Student> _students = new List<Student>()
    {
        new Student {Id = 1, Age = 19, Name = "John", Email = "John@example.cpom" }
    };

    public Student? Create(Student student)
    {
        _students.Add(student);
        return student;
    }

    public Student? GetById(int id)
    {
        return _students
            .Where(s => s.Id == id)
            .SingleOrDefault();
    }
}
