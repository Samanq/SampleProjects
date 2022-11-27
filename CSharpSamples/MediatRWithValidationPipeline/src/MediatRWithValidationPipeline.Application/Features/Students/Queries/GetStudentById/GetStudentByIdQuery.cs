using MediatR;
using MediatRWithValidationPipeline.Domain.Entities;

namespace MediatRWithValidationPipeline.Application.Features.Students.Queries.GetStudentById;

public record GetStudentByIdQuery(int Id) : IRequest<Student?>;
