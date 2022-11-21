using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);
