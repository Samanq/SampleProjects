using AuthorizationWithJwtSample.Application.Authentication.Interfaces;
using AuthorizationWithJwtSample.Application.Repositories;
using AuthorizationWithJwtSample.Infrastructure.Authentication;
using AuthorizationWithJwtSample.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthorizationWithJwtSample.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        // Registering UserRepository
        services.AddScoped<IUserRepository, UserRepository>();

        // Binding the JwtSettings
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        // Add IOption for getting JwtSettings
        //services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton(Options.Create(jwtSettings));

        // Registering the JwtTokenGenerator
        services.AddSingleton<IJwtTokenService, JwtTokenService>();

        // Registering the Authentication with JwtBearer
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer= true,
                ValidateAudience= true,
                ValidateLifetime= true,
                ValidateIssuerSigningKey= true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience= jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });

        return services;
    }
}