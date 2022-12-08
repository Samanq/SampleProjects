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
        var result = _authenticationService.Login(email, password);

        if (result is null)
        {
            return Unauthorized();
        }
        
        return Ok(result);
    }

    [HttpPost("RefreshToken")]
    public IActionResult RefreshToken(string refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
            return BadRequest("Invalid client request");

        //string accessToken = tokenApiModel.AccessToken;
        //string refreshToken = tokenApiModel.RefreshToken;
        var principal = _authenticationService.Get _jwt.GetPrincipalFromExpiredToken(accessToken);
        var username = principal.Identity.Name; //this is mapped to the Name claim by default
        var user = _userContext.LoginModels.SingleOrDefault(u => u.UserName == username);
        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            return BadRequest("Invalid client request");
        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshToken = newRefreshToken;
        _userContext.SaveChanges();
        return Ok(new AuthenticatedResponse()
        {
            Token = newAccessToken,
            RefreshToken = newRefreshToken
        });
    }

    [HttpPost("RevokeRefreshToken")]
    public IActionResult RevokeRefreshToken(string email)
    {
        var response = _authenticationService.RevokeRefreshToken(email);

        return Ok(response.Data);
    }
}
