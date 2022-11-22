using Mapster;
using MapsterSample.Contracts.Students;
using Microsoft.AspNetCore.Mvc;

namespace MapsterSample.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    [HttpGet("SimpleMapWithoutConfiguration")]
    public IActionResult SimpleMapWithoutConfiguration()
    {
        var studentCreateRequest = new StudentCreateRequest("John", "Doe", 10);

        StudentResponse studentResponse = studentCreateRequest.Adapt<StudentResponse>();


        return Ok(studentResponse);
    }

    [HttpGet("SimpleMapWithConfiguration")]
    public IActionResult SimpleMapWithConfiguration()
    {
        var studentCreateRequest = new StudentCreateRequest("John", "Doe", 10);

        var config = new TypeAdapterConfig();
        config.NewConfig<StudentCreateRequest, StudentShortResponse>()
            .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}");

        StudentShortResponse studentShortResponse = studentCreateRequest.Adapt<StudentShortResponse>(config);


        return Ok(studentShortResponse);
    }

    [HttpGet("SimpleMapWithGlobalConfiguration")]
    public IActionResult SimpleMapWithGlobalConfiguration()
    {
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


        return Ok(studentShortResponse);
    }

    [HttpGet("ConditionForMapping")]
    public IActionResult ConditionForMapping()
    {
        var studentCreateRequest = new StudentCreateRequest("John", "Doe", 10);

        // it only applies if the first name starts with "j"
        TypeAdapterConfig<StudentCreateRequest, StudentShortResponse>.NewConfig()
            .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}", src => src.FirstName.StartsWith("J", StringComparison.OrdinalIgnoreCase));

        StudentShortResponse studentShortResponse = studentCreateRequest.Adapt<StudentShortResponse>();

        return Ok(studentShortResponse);
    }
}
