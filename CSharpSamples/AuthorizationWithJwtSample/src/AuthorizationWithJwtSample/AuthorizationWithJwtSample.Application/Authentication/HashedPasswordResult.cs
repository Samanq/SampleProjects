namespace AuthorizationWithJwtSample.Application.Authentication;

public record HashedPasswordResult(
    byte[] PasswordHash,
    byte[] PasswordSalt);
