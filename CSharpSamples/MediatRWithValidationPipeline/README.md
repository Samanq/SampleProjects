# MediatR with validation pipeline

## Defining Validators
1. Install **FluentValidation.AspNetCore** Packge.

2. Create a class named **CreateStudentCommandValidator** in **Features/Students/Commands/CreateStudent** and implement the **AbstractValidator**
```C#
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
```

3. Register the FluentValidation in **DependecyInjection.cs**
```C#
// Registering all FluentValidation validators in the assembely.
services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
```
---

## Defining Validation Bahavior
1. In application layer Create a folder named **Behaviors**
2. Inside **Behaviors** folder create a class named **ValidationBehavior** and implement the **IPipelineBehavior<TRequest, TResponse>**
3. Then inject the IValidator into **ValidationBehavior**
```C#
public class ValidationBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class

{
    private readonly IValidator<TRequest>? _validator;

    // Injecting FluentValidation
    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validator is null)
        {
            return await next();
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            // any code before next() execute before the handler.
            return await next();
            // any code here execute after the handler
        }

        var errors = validationResult.Errors.ToList();
        
        throw new ValidationException(errors) ;
    }
}
```
