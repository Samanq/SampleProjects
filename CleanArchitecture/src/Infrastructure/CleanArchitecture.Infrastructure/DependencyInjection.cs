namespace CleanArchitecture.Infrastructure;

using CleanArchitecture.Application.Common.Interfaces.Authentication;
using CleanArchitecture.Application.Common.Interfaces.Services;
using CleanArchitecture.Infrastructure.Authentication;
using CleanArchitecture.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraStructure(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        return services;
    }
}