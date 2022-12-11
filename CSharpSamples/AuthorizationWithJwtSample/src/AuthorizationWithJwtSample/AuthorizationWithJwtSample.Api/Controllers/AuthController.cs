using AuthorizationWithJwtSample.Api.Dtos;
using AuthorizationWithJwtSample.Application.Authentication;
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
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        if (request is null) return BadRequest();

        var authResult = _authenticationService
            .Register(request.Name, request.Email, request.Password);

        SetRefreshTokenCookie(authResult.refreshToken);

        return Ok(authResult);
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var authResult = _authenticationService
            .Login(request.Email, request.Password);

        if (authResult is null) return Unauthorized();

        SetRefreshTokenCookie(authResult.refreshToken);

        return Ok(authResult);
    }

    [AllowAnonymous]
    [HttpPost("RefreshToken")]
    public IActionResult RefreshToken()
    {
        var rToken = Request.Cookies["refreshToken"];
        if (rToken is null)
        {
            return BadRequest("Invalid client request");
        }
        //if (string.IsNullOrEmpty(refreshToken))
        //{
        //    return BadRequest("Invalid client request");
        //}

        var authResult = _authenticationService
            .RefreshToken(GetAccessTokenFromHeader(), rToken);

        SetRefreshTokenCookie(authResult.refreshToken);

        return Ok(authResult);
    }

    [HttpPost("RevokeRefreshToken")]
    public IActionResult RevokeRefreshToken(string email)
    {
        var response = _authenticationService
            .RevokeRefreshToken(email);

        return Ok(response.Data);
    }

    private void SetRefreshTokenCookie(RefreshToken refreshToken)
    {
        var options = new CookieOptions
        {
            HttpOnly = true,
            Expires = refreshToken.ExipryDateTime
        };

        Response.Cookies.Append("refreshToken", refreshToken.Token, options);
    }
    private string GetAccessTokenFromHeader()
    {
        var keyword = "Bearer ";

        var currentAccessToken = HttpContext.Request.Headers["Authorization"]
            .ToString().Replace(keyword, string.Empty);

        return currentAccessToken;
    }
}
