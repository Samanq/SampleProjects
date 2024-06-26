﻿namespace AuthorizationWithJwtSample.Application.Authentication;

public record AuthenticationResult(
    string? accessToken,
    RefreshToken? refreshToken);
