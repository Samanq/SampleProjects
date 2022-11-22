using ApiValidationSample.Models;
using FluentValidation;

namespace ApiValidationSample.Validations;

public class DogToyValidator : AbstractValidator<DogToy>
{
	public DogToyValidator()
	{
		RuleFor(d => d.Name).NotNull().NotEmpty();
		RuleFor(d => d.HasSound).NotNull();
		RuleFor(d => d.Color)
			.NotNull()
			.MaximumLength(20)
			.MinimumLength(3);
	}
}
