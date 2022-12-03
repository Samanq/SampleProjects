using AuthorizationWithJwtSample.Application.Authentication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationWithJwtSample.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("Register")]
    public IActionResult Register(string name, string email, string password)
    {
        var authenticationResult = _authenticationService.Register(name, email, password);

        return Ok(authenticationResult);
    }

    [HttpPost("Login")]
    public IActionResult Login(string email, string password)
    {
        return Ok();
    }
}
