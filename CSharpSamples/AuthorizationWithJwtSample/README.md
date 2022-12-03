# Authentication and Authorization in ASP.Net Api with JWT

## Domain Layer
1. Create a class named **User** in **Entities** folder.
```C#
namespace AuthorizationWithJwtSample.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
```
---
## Application layer
1. Create an interface named **IJwtTokenGenerator** in **Authentication/Interfaces** folder.
```C#
namespace AuthorizationWithJwtSample.Application.Authentication.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
```
---

## Infrastructure
1. Install **System.IdentityModel.Tokens.Jwt** package.
2. Install **Microsoft.Extensions.Configuration** Package.
3. Install **Microsoft.Extensions.Options.ConfigurationExtensions** package.
4. Install **Microsoft.AspNetCore.Authentication.JwtBearer** packge

5. Create a class named **JwtSettings** in **Authentication** folder.
```C#
public class JwtSettings
{
    // Name of the section in appsettings.json
    public const string SectionName = "JwtSettings";

    public string Secret { get; init; } = string.Empty;
    public int ExpiryMinutes { get; init; }
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
}

```


6. Create a class named **JwtTokenGenerator** in **Authentication** folder and implement the **IJwtTokenGenerator**.
```C#
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    // Injecting JwtSettings
    public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
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
}
```

7. Create a class named **DependencyInjection** and register the services in AddInfrastructure method
```C#
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {

        // Binding the JwtSettings
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        // Add IOption for getting JwtSettings
        //services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton(Options.Create(jwtSettings));

        // Registering the JwtTokenGenerator
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        // Registering the Authentication with JwtBearer
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer= true,
                ValidateAudience= true,
                ValidateLifetime= true,
                ValidateIssuerSigningKey= true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience= jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });

        return services;
    }
}
```
---

## Api Layer
1. Install **Swashbuckle.AspNetCore.Filters** package. 
1. In Api project open **appsettings.json** and **appsettings.Development.json** then, add JwtSettings
```json
  "JwtSettings": {
    "Secret": "very-secret-key",
    "ExpiryMinutes": 60,
    "Issuer": "AuthorizationSample",
    "Audience": "AuthorizationSample"
  }
```
2. you can use **user-secrets** instead of setting the value in jwtSettings.
    - remove the value from **Secret**
    ```json
      "JwtSettings": {
        "Secret": "",
        "ExpiryMinutes": 60,
        "Issuer": "AuthorizationSample",
        "Audience": "AuthorizationSample"
    }
    ```
    - Right click on the api project to select **Manage User Secrets** in set this value
    ```json
    {
        "JwtSettings:Secret": "my-very-very-secret-key"
    }
    ```
    - Or you can do it with dotnet command:
    ```powershell
    dotnet user-sercrets set --project .\AuthorizationWithJwtSample.Api\ "JwtSettings:Secret" : "my-very-very-secret-key"
    ```
    - For viewing all user-sercrets you can use this command:
    ```powershell
    dotnet user-sercrets list --project .\AuthorizationWithJwtSample.Api\
    ```

3. In **Program.cs** add **UseAuthentication** and **UseAuthorization**
```C#
app.UseAuthentication();    // Adding Authentication
app.UseAuthorization();     // Adding Authorization.
```