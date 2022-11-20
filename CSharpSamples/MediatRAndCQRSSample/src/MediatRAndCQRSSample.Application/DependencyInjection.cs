using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MediatRAndCQRSSample.Application;

public static class DependencyInjection
{
    // Registering the application services
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        return services;
    }
}