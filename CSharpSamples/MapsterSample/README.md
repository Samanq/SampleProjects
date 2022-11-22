# Mapster Sample

## Simple Map
```C#
var studentCreateRequest = new StudentCreateRequest("John", "Doe", 10);
StudentResponse studentResponse = studentCreateRequest.Adapt<StudentResponse>();
```

## Simple Mapping with configuration
```C#
var studentCreateRequest = new StudentCreateRequest("John", "Doe", 10);

var config = new TypeAdapterConfig();
config.NewConfig<StudentCreateRequest, StudentShortResponse>()
    .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}");

StudentShortResponse studentShortResponse = studentCreateRequest.Adapt<StudentShortResponse>(config);
```

## Simple Mapping With Global Configuration
```C#
var studentCreateRequest = new StudentCreateRequest("John", "Doe", 10);

// Override Configuration to the GlobalSettings
var config = TypeAdapterConfig.GlobalSettings;
config.NewConfig<StudentCreateRequest, StudentShortResponse>()
    .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}");
// OR
// Append a Configuration to the GlobalSettings
config.ForType<StudentCreateRequest, StudentShortResponse>()
    .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}");
// OR
// Set the configuration for StudentCreateRequest to StudentShortResponse in GlobalSettings
TypeAdapterConfig<StudentCreateRequest, StudentShortResponse>.NewConfig()
    .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}");

StudentShortResponse studentShortResponse = studentCreateRequest.Adapt<StudentShortResponse>();
```

## Add conditions to mapping
```C#
var studentCreateRequest = new StudentCreateRequest("John", "Doe", 10);
        
// it only applies if the first name starts with "j"
TypeAdapterConfig<StudentCreateRequest, StudentShortResponse>.NewConfig()
    .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}", src => src.FirstName.StartsWith("J", StringComparison.OrdinalIgnoreCase));

StudentShortResponse studentShortResponse = studentCreateRequest.Adapt<StudentShortResponse>();
```

## Mapping with IMapper and dependency injection.
1. Install **Mapster.DependencyInjection** package.
2. Create a folder named **Mapping** and inside that create a class named **StudentMappingConfig** then Implement **IRegister**
```C#
public class StudentMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Defining the Mapping config
        config.NewConfig<StudentCreateRequest, StudentShortResponse>()
            .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}");
    }
}
```
3. Inside you controller inject the IMapper
```C#
[Route("[controller]")]
[ApiController]
public class StudentsWithIMapperController : ControllerBase
{
    private readonly IMapper _mapper;

    // Injecting IMapper
    public StudentsWithIMapperController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet("Get")]
    public IActionResult Get()
    {
        var studentCreateRequest = new StudentCreateRequest("John", "Doe", 10);

        var studentResponse = _mapper.Map<StudentResponse>(studentCreateRequest);

        return Ok(studentResponse);
    }
}
```
4. Inside Mapping folder create class named **DependencyInjection** and register the Mapster and its configuration into **AddMapping** method.
```C#
public static class DependencyInjection
{
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
```
5. In **Program.cs** register the **AddMapping** .