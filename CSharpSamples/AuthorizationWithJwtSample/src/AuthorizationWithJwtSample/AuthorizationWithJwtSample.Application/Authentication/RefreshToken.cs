namespace AuthorizationWithJwtSample.Application.Authentication;

public record RefreshToken(string Token, DateTime ExipryDateTime)
{
    public DateTime CreateDateTime { get; init; } = DateTime.UtcNow;
}
