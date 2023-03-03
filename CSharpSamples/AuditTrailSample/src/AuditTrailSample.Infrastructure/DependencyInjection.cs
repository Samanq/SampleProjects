using AuditTrailSample.Application.Repositories;
using AuditTrailSample.Infrastructure.Persistence.DataContexts;
using AuditTrailSample.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuditTrailSample.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrasructure(this IServiceCollection services)
    {
        services.AddDbContext<AuditTrailSampleDb>(options => 
        {
            options.UseSqlServer("Server=localhost;User ID=sa;Password=1234;Database=AuditTrailSample;MultipleActiveResultSets=true;TrustServerCertificate=true");
        });

        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}