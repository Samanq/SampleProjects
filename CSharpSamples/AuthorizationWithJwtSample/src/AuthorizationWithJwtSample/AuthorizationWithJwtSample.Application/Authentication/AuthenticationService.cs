using AuthorizationWithJwtSample.Application.Authentication.Interfaces;
using AuthorizationWithJwtSample.Domain.Entities;

namespace AuthorizationWithJwtSample.Application.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }


    public AuthenticationResult Login(string email, string password)
    {
        throw new NotImplementedException();
    }

    public AuthenticationResult Register(string name, string email, string password)
    {
        var user = new User
        {
            Name = name,
            Email= email,
            Password = password 
        };

        var token = _jwtTokenGenerator.GenerateToken(user);

        var result = new AuthenticationResult(user, token);

        return result;
    }
}
