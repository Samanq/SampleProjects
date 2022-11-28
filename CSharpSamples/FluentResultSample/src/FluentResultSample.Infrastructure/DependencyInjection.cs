using FluentResultSample.Application.Repositories;
using FluentResultSample.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FluentResultSample.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IStudentRepository, StudentRepository>();
        return services;
    }

}