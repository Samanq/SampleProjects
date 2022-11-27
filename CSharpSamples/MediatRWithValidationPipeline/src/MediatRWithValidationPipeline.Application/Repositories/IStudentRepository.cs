using MediatRWithValidationPipeline.Domain.Entities;

namespace MediatRWithValidationPipeline.Application.Repositories;

public interface IStudentRepository
{
    Student? GetById(int id);
    Student? Create(Student student);
}
