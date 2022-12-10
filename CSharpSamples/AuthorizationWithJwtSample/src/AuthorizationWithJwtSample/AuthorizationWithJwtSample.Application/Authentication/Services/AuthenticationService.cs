using AuthorizationWithJwtSample.Application.Authentication.Interfaces;
using AuthorizationWithJwtSample.Application.Common;
using AuthorizationWithJwtSample.Application.Repositories;
using AuthorizationWithJwtSample.Domain.Entities;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthorizationWithJwtSample.Application.Authentication.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenService jwtTokenService, IUserRepository userRepository)
    {
        _jwtTokenService = jwtTokenService;
        _userRepository = userRepository;
    }


    public AuthenticationResult Login(string email, string password)
    {
        var user = _userRepository.GetByEmail(email);

        if (user is null)
        {
            throw new Exception("Wrong email or password");
        }

        if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        {
            throw new Exception("Wrong email or password");
        }

        var accessToken = _jwtTokenService.GenerateAccessToken(user);
        var refreshToken = _jwtTokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken.Token;
        user.RefreshTokenExpiryDate = refreshToken.ExipryDateTime;

        var updatedUser = _userRepository.Update(user);

        return new AuthenticationResult(accessToken, updatedUser?.RefreshToken);
    }
    public AuthenticationResult Register(string name, string email, string password)
    {
        var hashedPassword = CreatePasswordHash(password);
        var refreshToken = _jwtTokenService.GenerateRefreshToken();

        var user = new User
        {
            Name = name,
            Email = email,
            PasswordHash = hashedPassword.PasswordHash,
            PasswordSalt = hashedPassword.PasswordSalt,
            RefreshToken = refreshToken.Token,
            RefreshTokenExpiryDate = refreshToken.ExipryDateTime,
        };

        var accessToken = _jwtTokenService.GenerateAccessToken(user);

        var createdUser = _userRepository.Create(
            user.Name,
            user.Email,
            user.PasswordHash,
            user.PasswordSalt,
            user.RefreshToken,
            user.RefreshTokenExpiryDate);

        if (createdUser is null)
        {
            throw new Exception("User cannot be create.");
        }

        var authResult = new AuthenticationResult(accessToken, createdUser.RefreshToken);

        return authResult;
    }
    public HashedPasswordResult CreatePasswordHash(string password)
    {
        using (var hmac = new HMACSHA512())
        {
            var passwrodSalt = hmac.Key;
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return new HashedPasswordResult(passwordHash, passwrodSalt);
        }
    }
    public bool VerifyPasswordHash(string password, byte[]? passwordHash, byte[]? passwordSalt)
    {
        if (passwordHash is null || passwordSalt is null)
        {
            return false;
        }

        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
    public AuthenticationResult RefreshToken(int userId, string accessToken, string refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
            throw new ArgumentNullException("Refresh token connot be null");

        var principal = _jwtTokenService.GetPrincipalFromExpiredToken(accessToken);

        if (principal is null || !principal.Claims.Any())
        {
            throw new Exception("Bad Token");
        }

        var userEmail = principal?.Claims
        .FirstOrDefault(p => p.Type == ClaimValueTypes.Email)?.Value;

        var user = _userRepository.GetByEmail(userEmail);

        if (user is null ||
            user.RefreshToken != refreshToken ||
            user.RefreshTokenExpiryDate <= DateTime.UtcNow)
        {
            throw new Exception("Bad token");
        }

        var newAccessToken = _jwtTokenService.GenerateAccessToken(user);
        var newRefreshToken = _jwtTokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken.Token;

        _userRepository.Update(user);

        var authResult = new AuthenticationResult(newAccessToken, newRefreshToken.Token);

        return authResult;
    }
    public Response<User?> RevokeRefreshToken(string email)
    {
        var user = _userRepository.GetByEmail(email);

        if (user is null)
        {
            return new Response<User?>(false, "user cannot be found!", null);
        }

        user.RefreshToken = null;
        user.RefreshTokenExpiryDate = null;

        _userRepository.Update(user);

        return new Response<User?>(true, $"Refresh token revoked for {user.Name}", user);
    }
}
