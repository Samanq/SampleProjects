namespace AuthorizationWithJwtSample.Application.Authentication;

public record RefreshToken(
    string Token,
    DateTime ExipryDateTime);
