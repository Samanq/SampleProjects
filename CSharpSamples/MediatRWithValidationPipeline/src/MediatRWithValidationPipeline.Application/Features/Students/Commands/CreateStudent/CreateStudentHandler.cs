using MediatR;
using MediatRWithValidationPipeline.Application.Repositories;
using MediatRWithValidationPipeline.Domain.Entities;

namespace MediatRWithValidationPipeline.Application.Features.Students.Commands.CreateStudent;

public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, Student?>
{
    private readonly IStudentRepository _studentRepository;

    public CreateStudentHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }


    public Task<Student?> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(
            _studentRepository.Create(new Student
            {
                Id = 0,
                Age = request.Age,
                Email = request.Email,
                Name = request.Name,
            }));
    }
}
