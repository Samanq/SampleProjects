namespace CleanArchitecture.Application;

using CleanArchitecture.Application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    // Registering every service in application layer
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}
