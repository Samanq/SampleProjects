using Mapster;
using MapsterMapper;
using System.Reflection;

namespace CleanArchitecture.Api.Common.Mapping;

public static class DependencyInjection
{
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        // Getting Mapster global settings
        var config = TypeAdapterConfig.GlobalSettings;

        // Scaning the Executing Assembly And collect All IRegisters
        config.Scan(Assembly.GetExecutingAssembly());

        // Registering the Mapster configurations
        services.AddSingleton(config);

        // Registering the IMapper
        services.AddSingleton<IMapper, ServiceMapper>();

        return services;
    }
}
