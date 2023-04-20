using SonarQubeSample.Domain.Entities;

namespace SonarQubeSample.Infrastructure.Persistent;

public class StudentRepository
{
    private static List<Student> _students = new List<Student>
    {
        new Student
        {
            Id = 1, FirstName = "John", LastName = "Doe"
        },
        new Student
        {
            Id = 2, FirstName = "Jane", LastName = "Doe"
        },
        new Student
        {
            Id = 3, FirstName = "Peter", LastName = "Jackson"
        },
    };

    public IEnumerable<Student> GetAll()
    {
        return _students;
    }

    public Student? GetById(long id)
    {
        return _students.SingleOrDefault(student => student.Id == id);
    }

    public void Create(Student student)
    {
        _students.Add(student);
    }

    public void Edit(long id, Student student)
    {
        _students.Add(student);
    }

    public void Delete(long id)
    {
        var student = _students.SingleOrDefault(student => student.Id == id);

        _students.Remove(student);
    }
}
