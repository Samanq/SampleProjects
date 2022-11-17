using ApiValidationSample.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiValidationSample.Attributes;

public class StudentTypeValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var student = validationContext.ObjectInstance as Student;

        if (student?.Type?.ToLower() == "full-time")
        {
            return new ValidationResult("full-time is not allowed");
        }

        return ValidationResult.Success;
    }
}
