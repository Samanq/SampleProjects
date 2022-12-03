using AuthorizationWithJwtSample.Domain.Entities;

namespace AuthorizationWithJwtSample.Application.Authentication
{
    public record AuthenticationResult(User user, string token);
    
}
