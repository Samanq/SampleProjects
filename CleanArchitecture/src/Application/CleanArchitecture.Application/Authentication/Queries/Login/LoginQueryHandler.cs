using CleanArchitecture.Application.Authentication.Common;
using CleanArchitecture.Application.Common.Interfaces.Authentication;
using CleanArchitecture.Application.Common.Interfaces.Persistence;
using CleanArchitecture.Domain.Common.Errors;
using CleanArchitecture.Domain.Entities;
using ErrorOr;
using MediatR;

namespace CleanArchitecture.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        if (_userRepository.GetByEmail(query.Email) is not User user)
        {
            // Return one error
            return Errors.Authentication.InvalidCredential;
        }

        if (user.Password != query.Password)
        {
            // Return list of errors
            return new[] { Errors.Authentication.InvalidCredential };
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}
