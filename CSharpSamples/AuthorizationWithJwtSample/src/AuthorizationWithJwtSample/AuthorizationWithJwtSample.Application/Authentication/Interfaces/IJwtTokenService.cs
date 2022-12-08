using AuthorizationWithJwtSample.Domain.Entities;
using System.Security.Claims;

namespace AuthorizationWithJwtSample.Application.Authentication.Interfaces;

public interface IJwtTokenService
{
    string GenerateAccessToken(User user);
    RefreshToken GenerateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
