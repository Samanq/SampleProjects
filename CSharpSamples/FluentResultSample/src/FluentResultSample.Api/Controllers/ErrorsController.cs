using Microsoft.AspNetCore.Mvc;

namespace FluentResultSample.Api.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}
