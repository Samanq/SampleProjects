using System.ComponentModel.DataAnnotations;

namespace ApiValidationSample.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
    public class FutureAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            return value is DateTime dateTime && dateTime > DateTime.UtcNow
                ? ValidationResult.Success
                : new ValidationResult("Date mus be in the future");
        }
    }
}
