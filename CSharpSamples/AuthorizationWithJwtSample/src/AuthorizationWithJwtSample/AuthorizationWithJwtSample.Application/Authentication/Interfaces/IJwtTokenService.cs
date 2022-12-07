using AuthorizationWithJwtSample.Domain.Entities;

namespace AuthorizationWithJwtSample.Application.Authentication.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
    RefreshToken GenerateRefreshToken();
}
