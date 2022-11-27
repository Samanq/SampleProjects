using FluentValidation;
using MediatR;

namespace MediatRWithValidationPipeline.Application.Behaviors;

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
