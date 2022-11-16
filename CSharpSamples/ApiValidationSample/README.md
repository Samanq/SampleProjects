# Dotnet WebApi Validation

## Validation Attributes
Create a Student class as a model and use validation attribute for validation

```C#
public class Student
{
    [Required(AllowEmptyStrings = true)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(10)] // Max 10 Charecters
    public string LastName { get; set; } = string.Empty;

    [Required]
    [Range(10,99)] // Number between 10 to 99
    public int Age { get; set; }

    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;

    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage ="Code should contain only a-z letters")]
    public string Code { get; set; } = string.Empty;
}
```

## Implementing IValidatableObject
```C#
public class Teacher : IValidatableObject
{
    public string Name { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (BirthDate.Year < 2015)
        {
            yield return new ValidationResult("Date is incorrect");
        }
    }
}
```

## Custom Validation Attribute
Create a new class name StudentTypeValidationAttribute and inherite from ValidationAttribute, then override IsValid method.

```C#
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
```
Then use the attribute over your field.
```C#
public class Student
{
    [StudentTypeValidation]
    public string Type { get; set; } = string.Empty;
}
```

## Validation in parameter
you can user validation attributes in your parameters.

```C#
[HttpGet("GetByLastName")]
public IActionResult GetByLastName([StringLength(10)] string LastName)
{
    return Ok(_students
        .Where(s => s.LastName.ToLower() == LastName.ToLower()));
}
```
## Fluent Validation
1. Install FluentValidation.AspNetCore Package
2. Create a Class named dog as model
```C#
public class Dog
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Color { get; set; } = string.Empty;
}
```
3. Create a class named DogValidator and inherite from AbstractValidator
```C#
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
```