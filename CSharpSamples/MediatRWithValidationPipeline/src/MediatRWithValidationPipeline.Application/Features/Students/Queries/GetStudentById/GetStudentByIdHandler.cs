using MediatR;
using MediatRWithValidationPipeline.Application.Repositories;
using MediatRWithValidationPipeline.Domain.Entities;

namespace MediatRWithValidationPipeline.Application.Features.Students.Queries.GetStudentById;

public class GetStudentByIdHandler : IRequestHandler<GetStudentByIdQuery, Student?>
{
    private readonly IStudentRepository _studentRepository;

    public GetStudentByIdHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }


    public Task<Student?> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        return Task
            .FromResult(_studentRepository.GetById(request.Id));
    }
}
