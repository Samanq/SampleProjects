namespace CleanArchitecture.Application.Common.Interfaces.Authentication;

using CleanArchitecture.Domain.Entities;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
