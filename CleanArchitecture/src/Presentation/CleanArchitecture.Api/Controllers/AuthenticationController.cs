using CleanArchitecture.Application.Authentication.Commands.Register;
using CleanArchitecture.Application.Authentication.Common;
using CleanArchitecture.Application.Authentication.Queries.Login;
using CleanArchitecture.Contracts.Authentication;
using CleanArchitecture.Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers;
[Route("[controller]")]
public class AuthenticationController : ApiController
{
    // Using ISender instead of IMediator
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody]LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(query);

        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredential)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: authResult.FirstError.Description);
        }
        return authResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors));
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult>  Register([FromBody] RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);

        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

        return authResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors));
    }
}
