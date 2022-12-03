using AuthorizationWithJwtSample.Domain.Entities;

namespace AuthorizationWithJwtSample.Application.Authentication.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
