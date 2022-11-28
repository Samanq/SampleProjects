using FluentResultSample.Application.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FluentResultSample.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}