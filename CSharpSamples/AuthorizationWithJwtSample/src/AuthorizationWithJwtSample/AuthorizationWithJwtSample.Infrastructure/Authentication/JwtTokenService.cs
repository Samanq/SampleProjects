using AuthorizationWithJwtSample.Application.Authentication;
using AuthorizationWithJwtSample.Application.Authentication.Interfaces;
using AuthorizationWithJwtSample.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthorizationWithJwtSample.Infrastructure.Authentication;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _jwtSettings;

    // Injecting JwtSettings
    public JwtTokenService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateToken(User user)
    {
        // Creating signing credential 
        var signingCredential = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);

        // Creating the claims
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.Name),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, "User")
        };


        // Creating security token
        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCredential);

        // Creating the token result.
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public RefreshToken GenerateRefreshToken()
    {
        var refreshToken = new RefreshToken(
            Token: Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            ExipryDateTime: DateTime.Now.AddMinutes(10));

        return refreshToken;
    }
}
