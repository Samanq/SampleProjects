using CleanArchitecture.Application.Common.Errors;
using ErrorOr;

namespace CleanArchitecture.Application.Services.Authentication;

public interface IAuthenticationService
{
    ErrorOr<AuthenticationResult> Login(string email, string password);
    ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
}
