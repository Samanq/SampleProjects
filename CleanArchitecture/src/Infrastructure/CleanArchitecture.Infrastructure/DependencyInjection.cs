namespace CleanArchitecture.Infrastructure;

using CleanArchitecture.Application.Common.Interfaces.Authentication;
using CleanArchitecture.Application.Common.Interfaces.Persistence;
using CleanArchitecture.Application.Common.Interfaces.Services;
using CleanArchitecture.Infrastructure.Authentication;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraStructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}