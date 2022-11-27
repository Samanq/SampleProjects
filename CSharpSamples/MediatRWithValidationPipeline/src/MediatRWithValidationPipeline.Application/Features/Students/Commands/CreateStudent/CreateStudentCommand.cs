using MediatR;
using MediatRWithValidationPipeline.Domain.Entities;

namespace MediatRWithValidationPipeline.Application.Features.Students.Commands.CreateStudent;

public record CreateStudentCommand(string Name, string Email, int Age) : IRequest<Student?>;
