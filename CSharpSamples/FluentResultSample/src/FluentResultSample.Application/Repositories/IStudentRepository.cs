using FluentResults;
using FluentResultSample.Application.Dtos;
using FluentResultSample.Domain.Entities;

namespace FluentResultSample.Application.Repositories;

public interface IStudentRepository
{
    Student? GetById(int id);
    Student? GetByEmail(string email);

    // It returns s student or list of errors
    Result<Student> Create(CreateStudentDto dto);
}
