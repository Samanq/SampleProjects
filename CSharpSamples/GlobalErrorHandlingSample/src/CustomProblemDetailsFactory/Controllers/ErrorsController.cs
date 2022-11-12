using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CustomProblemDetailsFactory.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)] // We add this because if we define an action without http method attribute, swagger will not run properly
public class ErrorsController : ControllerBase
{
    // No Http Method
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        return Problem(title: exception?.Message);
    }
}
