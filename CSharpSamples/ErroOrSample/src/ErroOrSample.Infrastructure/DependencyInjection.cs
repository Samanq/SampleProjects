using ErroOrSample.Application.Repositories;
using ErroOrSample.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ErroOrSample.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IStudentRepository, StudentRepository>();
        return services;
    }
}