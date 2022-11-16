using ApiValidationSample.Models;
using FluentValidation;

namespace ApiValidationSample.Validators
{
    public class DogValidator : AbstractValidator<Dog>
    {
        public DogValidator()
        {
            RuleFor(d => d.Color).NotNull().NotEmpty();
            RuleFor(d => d.OwnerEmail).EmailAddress();
            RuleFor(d => d.Color).NotNull().MaximumLength(10);
            RuleFor(d => d.Color).Must(d => d?.ToLower().Contains("color") == true);
        }
    }
}
