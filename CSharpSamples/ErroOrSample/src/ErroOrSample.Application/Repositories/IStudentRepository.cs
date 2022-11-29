using ErroOrSample.Domian.Entities;
using ErrorOr;

namespace ErroOrSample.Application.Repositories;

public interface IStudentRepository
{
    Student? GetById(int id);
    Student? GetByEmail(string email);

    // Return an Error or a student
    ErrorOr<Student> Create(string name, string email, int age);
}
