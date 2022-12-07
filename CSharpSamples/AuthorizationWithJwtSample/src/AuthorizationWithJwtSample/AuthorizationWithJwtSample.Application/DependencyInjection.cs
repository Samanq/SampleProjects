using AuthorizationWithJwtSample.Application.Authentication.Interfaces;
using AuthorizationWithJwtSample.Application.Authentication.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AuthorizationWithJwtSample.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }
}