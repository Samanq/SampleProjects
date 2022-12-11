using AuthorizationWithJwtSample.Application.Common;
using AuthorizationWithJwtSample.Domain.Entities;

namespace AuthorizationWithJwtSample.Application.Authentication.Interfaces;

public interface IAuthenticationService
{
    AuthenticationResult Register(string name, string email, string password);
    AuthenticationResult Login(string email, string password);
    HashedPasswordResult CreatePasswordHash(string password);
    bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    AuthenticationResult RefreshToken(string accessToken, string refreshToken);
    Response<User?> RevokeRefreshToken(string email);
}
