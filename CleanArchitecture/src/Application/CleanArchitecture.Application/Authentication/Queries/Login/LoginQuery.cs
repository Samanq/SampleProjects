using CleanArchitecture.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace CleanArchitecture.Application.Authentication.Queries.Login;

public record LoginQuery(string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;

