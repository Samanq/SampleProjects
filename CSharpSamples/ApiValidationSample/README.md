# Dotnet WebApi Validation

## Validation Attributes (Data Annotation)
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

    [MinLength(10), MaxLength(50)]
    public string Description { get; set; } = string.Empty;
}
```
---
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
---

## Custom Validation Attribute
Create a new class name StudentTypeValidationAttribute and inherite from **ValidationAttribute**, then override **IsValid** method.

```C#
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
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
---
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
---

## Fluent Validation
1. Install **FluentValidation.AspNetCore** Package.
2. Create a Class named dog as model.
```C#
public class Dog
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Color { get; set; } = string.Empty;
}
```
3. Create a class named DogValidator and inherite from **AbstractValidator**, then, write the rules inside the **constructor** .
```C#
public class DogValidator : AbstractValidator<Dog>
{
    public DogValidator()
    {
        RuleFor(d => d.Color).NotNull().NotEmpty();
        RuleFor(d => d.OwnerEmail).EmailAddress();
        RuleFor(d => d.Color).NotNull().MaximumLength(20);
        RuleFor(d => d.Description).Length(10,50);

        // Include other validation class for readability
        Include(new DogComplexValidator());

        // Validating a collection member
        RuleForEach(d => d.DogToys).SetValidator(new DogToyValidator());
    }
}

// We can have mupltiple validator in merge them with include.
public class DogComplexValidator : AbstractValidator<Dog>
{
    public DogComplexValidator()
    {
        RuleFor(d => d.Color)
            .Must(d => d?.ToLower().Contains("color") == true)
            .WithMessage("Color must contains a word 'color'");
    }
}
```
4. Register the validator in **program.cs**
```C#
// Register the FluentValidation
builder.Services.AddFluentValidationAutoValidation();
// Register from the assembly.
builder.Services.AddValidatorsFromAssemblyContaining<DogValidator>();
// Or register a single validator
//builder.Services.AddScoped<IValidator<Dog>, DogValidator>();
```

## Validating outside the controller
1. Create a class for dogToys and Inject the **IValidator**.
```C#
public class DogToyService : IDogToyService
{
    private static List<DogToy> _dogToys = new List<DogToy>()
    {
        new DogToy
        {
            Name= "Ball",
            Color = "Red",
            HasSound = true
        }
    };

    private readonly IValidator<DogToy> _validator;

    // Injecting the IValidaor
    public DogToyService(IValidator<DogToy> validator)
    {
        _validator = validator;
    }

    public async Task Create(DogToy dogToy)
    {
        // throw exception if validation fails.
        await _validator.ValidateAndThrowAsync(dogToy);

        _dogToys.Add(dogToy);
    }
}
```
2. Create the DogToysController and inject the IDogToyServiuce,  then, call the Create Method
```C#
[Route("[controller]")]
[ApiController]
public class DogToysController : ControllerBase
{
    private readonly IDogToyService _dogToyService;

    public DogToysController(IDogToyService dogToyService)
    {
        _dogToyService = dogToyService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name, string color)
    {
        // Create method inside the DogToyService is responsible for validation
        await _dogToyService.Create(new Models.DogToy
        { 
            Name = name,
            Color = color,
            HasSound = true 
        });

        return Ok(_dogToyService.GetAll());
    }

}
```
3. Register the IDogService in Program.cs
```C#
// Registering IDogToyService
builder.Services.AddScoped<IDogToyService, DogToyService>();
```