using FluentValidation;

namespace MediatRWithValidationPipeline.Application.Features.Students.Commands.CreateStudent;

public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
	public CreateStudentCommandValidator()
	{
		RuleFor(s => s.Email)
			.EmailAddress()
			.NotNull()
			.NotEmpty();

		RuleFor(s => s.Name)
			.Length(2, 10)
			.NotNull()
			.NotEmpty();

		RuleFor(s => s.Age)
			.InclusiveBetween(18, 99)
			.NotNull()
			.NotEmpty();
	}
}
