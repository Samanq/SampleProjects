using ApiValidationSample.Models;
using FluentValidation;

namespace ApiValidationSample.Validations;

public class DogValidator : AbstractValidator<Dog>
{
    public DogValidator()
    {
        

        RuleFor(d => d.Color).NotNull().NotEmpty();
        RuleFor(d => d.OwnerEmail).EmailAddress();
        RuleFor(d => d.Color).NotNull().MaximumLength(20);
        RuleFor(d => d.Description).Length(10, 50);
        //Include(new DogComplexValidator()); // Is not working
        RuleFor(d => d.Color)
            .Must(d => d?.ToLower().Contains("color") == true)
            .WithMessage("Color must contains a word 'color'");

        // Validating a collection member
        RuleForEach(d => d.DogToys).SetValidator(new DogToyValidator());
    }
}

// We can have mupltiple validator in merge them with include.
//public class DogComplexValidator : AbstractValidator<Dog>
//{
//    public DogComplexValidator()
//    {
//        RuleFor(d => d.Color)
//            .Must(d => d?.ToLower().Contains("color") == true)
//            .WithMessage("Color must contains a word 'color'");
//    }
//}
