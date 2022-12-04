using Microsoft.Extensions.DependencyInjection;

namespace BlazorSample.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services) 
    {
        return services;
    }
}