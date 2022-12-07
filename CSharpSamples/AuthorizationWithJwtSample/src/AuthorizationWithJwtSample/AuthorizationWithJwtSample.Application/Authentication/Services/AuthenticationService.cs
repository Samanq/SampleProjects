using AuthorizationWithJwtSample.Application.Authentication.Interfaces;
using AuthorizationWithJwtSample.Application.Repositories;
using AuthorizationWithJwtSample.Domain.Entities;
using System.Reflection;
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

        if (VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        {
            return new AuthenticationResult(user, _jwtTokenService.GenerateToken(user));
        }

        throw new Exception("Something went wrong in login");
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

        var token = _jwtTokenService.GenerateToken(user);

        var createResult = _userRepository.Create(
            user.Name,
            user.Email,
            user.PasswordHash,
            user.PasswordSalt,
            user.RefreshToken,
            user.RefreshTokenExpiryDate);

        var authResult = new AuthenticationResult(createResult, token);

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
}
