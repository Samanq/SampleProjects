using Microsoft.Extensions.DependencyInjection;

namespace BlazorSample.Infrastructure.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddIfrastructure(this IServiceCollection services) 
    {
        return services;
    }
}