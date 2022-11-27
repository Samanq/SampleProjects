using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using MediatRWithValidationPipeline.Application.Behaviors;
using MediatRWithValidationPipeline.Application.Features.Students.Commands.CreateStudent;
using MediatRWithValidationPipeline.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MediatRWithValidationPipeline.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Registering all mediators in this assembly
        services.AddMediatR(typeof(DependencyInjection).Assembly);

        // Registering the CreateStudentCommand Behavior
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        // Registering all FluentValidation validators in the assembely.
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}