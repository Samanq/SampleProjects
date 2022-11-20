using MediatRAndCQRSSample.Application.Repositories;
using MediatRAndCQRSSample.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MediatRAndCQRSSample.Infrastructure;

public static class DependencyInjection
{
    // Registering Infrastructure services
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IPesonRepository, PersonRepository>();
        return services;
    }
}
