using MediatRWithValidationPipeline.Application.Repositories;
using MediatRWithValidationPipeline.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MediatRWithValidationPipeline.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service)
    {
        // Regitering Student Repository
        service.AddScoped<IStudentRepository, StudentRepository>();

        return service;
    }
}