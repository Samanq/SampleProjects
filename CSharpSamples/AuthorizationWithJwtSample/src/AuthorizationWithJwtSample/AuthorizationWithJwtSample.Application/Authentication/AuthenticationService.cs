using AuthorizationWithJwtSample.Application.Authentication.Interfaces;
using AuthorizationWithJwtSample.Application.Repository;
using AuthorizationWithJwtSample.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace AuthorizationWithJwtSample.Application.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }


    public AuthenticationResult Login(string email, string password)
    {
        throw new NotImplementedException();
    }
    public AuthenticationResult Register(string name, string email, string password)
    {
        var hashedPassword = CreatePasswordHash(password);

        var user = new User
        {
            Name = name,
            Email= email,
            PasswordHash = hashedPassword.PasswordHash,
            PasswordSalt = hashedPassword.PasswordSalt,
            // TODO Fix RefreshToken and ExpiryDateTime
            RefreshToken = "asd",
            RefreshTokenExpiryDate= DateTime.UtcNow,
        };

        var token = _jwtTokenGenerator.GenerateToken(user);

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
    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
