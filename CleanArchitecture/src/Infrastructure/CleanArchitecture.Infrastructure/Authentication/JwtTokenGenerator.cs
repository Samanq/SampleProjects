namespace CleanArchitecture.Infrastructure.Authentication;

using CleanArchitecture.Application.Common.Interfaces.Authentication;
using CleanArchitecture.Application.Common.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string GenerateToken(Guid userId, string firstName, string lastName)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var signinCredential = new SigningCredentials(
            new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("super-secret-key")),
            SecurityAlgorithms.HmacSha256);


        var securityToken = new JwtSecurityToken(
            issuer: "CleanArchitecture",
            audience: "CleanArchitecture",
            claims: claims,
            signingCredentials: signinCredential,
            expires: _dateTimeProvider.Now.AddDays(1)
            );


        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
