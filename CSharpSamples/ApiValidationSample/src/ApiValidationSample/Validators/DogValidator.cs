using ApiValidationSample.Models;
using FluentValidation;

namespace ApiValidationSample.Validators;

public class DogValidator : AbstractValidator<Dog>
{
    public DogValidator()
    {
        RuleFor(d => d.Color).NotNull().NotEmpty();
        RuleFor(d => d.OwnerEmail).EmailAddress();
        RuleFor(d => d.Color).NotNull().MaximumLength(20);

        // Include other validation class for readability
        Include(new DogComplexValidator());

        // Validating a collection member
        RuleForEach(d => d.DogToys).SetValidator(new DogToyValidator());
    }
}

public class DogComplexValidator : AbstractValidator<Dog>
{
    public DogComplexValidator()
    {
        RuleFor(d => d.Color)
            .Must(d => d?.ToLower().Contains("color") == true)
            .WithMessage("Color must contains a word 'color'");
    }
}
