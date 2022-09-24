using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token);
