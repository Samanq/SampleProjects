using CleanArchitecture.Api.Common.Errors;
using CleanArchitecture.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CleanArchitecture.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSingleton<ProblemDetailsFactory, SampleProblemDetailsFactory>();
        services.AddMapping();

        return services;
    }
}
