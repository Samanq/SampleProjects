using AuthorizationWithJwtSample.Application.Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        var result = _authenticationService.Login(email, password);

        if (result is null)
        {
            return Unauthorized();
        }
        
        return Ok(result);
    }

    [Authorize]
    [HttpPost("RefreshToken/{userId}")]
    public IActionResult RefreshToken(int userId,[FromBody] string refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
            return BadRequest("Invalid client request");

        var newAccessToken = _authenticationService
            .RefreshToken(userId, GetAccessTokenFromHeader(), refreshToken);
        
       return Ok(newAccessToken);
    }

    [HttpPost("RevokeRefreshToken")]
    public IActionResult RevokeRefreshToken(string email)
    {
        var response = _authenticationService
            .RevokeRefreshToken(email);

        return Ok(response.Data);
    }

    private string GetAccessTokenFromHeader()
    {
        var keyword = "Bearer ";

        var currentAccessToken = HttpContext.Request.Headers["Authorization"]
            .ToString().Replace(keyword, string.Empty);

        return currentAccessToken;
    }
}
