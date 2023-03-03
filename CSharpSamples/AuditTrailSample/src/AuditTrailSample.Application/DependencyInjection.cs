using Microsoft.Extensions.DependencyInjection;

namespace AuditTrailSample.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}