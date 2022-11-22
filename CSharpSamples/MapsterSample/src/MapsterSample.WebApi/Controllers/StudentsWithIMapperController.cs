using Mapster;
using MapsterMapper;
using MapsterSample.Contracts.Students;
using Microsoft.AspNetCore.Mvc;

namespace MapsterSample.WebApi.Controllers;

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

        var studentResponse = _mapper.Map<StudentShortResponse>(studentCreateRequest);

        return Ok(studentResponse);
    }
}
