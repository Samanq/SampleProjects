using CleanArchitecture.Application.Authentication.Commands.Register;
using CleanArchitecture.Application.Authentication.Common;
using CleanArchitecture.Application.Authentication.Queries.Login;
using CleanArchitecture.Contracts.Authentication;
using CleanArchitecture.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers;
[Route("[controller]")]
public class AuthenticationController : ApiController
{
    // Using ISender instead of IMediator
    //private readonly IMediator _mediator;
    private readonly ISender _mediator;


    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody]LoginRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(query);

        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredential)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: authResult.FirstError.Description);
        }
        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult>  Register([FromBody] RegisterRequest request)
    {
        var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

        return authResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
                    authResult.User.Id,
                    authResult.User.FirstName,
                    authResult.User.LastName,
                    authResult.User.Email,
                    authResult.Token);
    }
}
