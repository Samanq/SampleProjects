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
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }

    // When the access token expires, we can use refresh token to get a new access token from the authentication controller.
    // Whenever the user login into the application using valid credentials, we will update refresh token and token expiry time.
    // If something went wrong, the refresh token can be revoked which means that when the application tries to use it to get a new access token, that request will be rejected, and the user  have to authenticate again.
    public string? RefreshToken { get; set; } = string.Empty;
    // The lifetime of a refresh token is usually much longer than the lifetime of an access token.
    public DateTime? RefreshTokenExpiryDate { get; set; }
}
```
---
## Application layer
1. Create a class named **Response\<T>** in **Common** folder.
```C#
namespace AuthorizationWithJwtSample.Application.Common;

public class Response<T>
{
    public string? Message { get; init; }
    public bool IsSuccess { get; init; }
    public T? Data { get; init; }

    public Response(bool isSuccess, string message, T? data)
    {
        Message= message;
        IsSuccess = isSuccess;
        Data = data;
    }
}
```

2. Create a record named **RefreshToken** in **Authentication** folder.
```C#
namespace AuthorizationWithJwtSample.Application.Authentication;

public record RefreshToken(string Token, DateTime? ExipryDateTime)
{
    public DateTime? CreateDateTime { get; init; } = DateTime.UtcNow;
}
```

3. Create a record named **HashedPasswordResult** in **Authentication** folder.
```C#
namespace AuthorizationWithJwtSample.Application.Authentication;

public record HashedPasswordResult(
    byte[] PasswordHash,
    byte[] PasswordSalt);
```

4. Create a record named **AuthenticationResult** in **Authentication** folder.
```C#
namespace AuthorizationWithJwtSample.Application.Authentication;

public record AuthenticationResult(
    string? accessToken,
    RefreshToken? refreshToken);
```

5. Create an interface named **IJwtTokenService** in **Authentication/Interfaces** folder.
```C#
namespace AuthorizationWithJwtSample.Application.Authentication.Interfaces;

public interface IJwtTokenService
{
    string GenerateAccessToken(User user);
    RefreshToken GenerateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
```

6. Create an interface named **IAuthenticationService** in **Authentication/Interfaces** folder.
```C#
namespace AuthorizationWithJwtSample.Application.Authentication.Interfaces;

public interface IAuthenticationService
{
    AuthenticationResult Register(string name, string email, string password);
    AuthenticationResult Login(string email, string password);
    HashedPasswordResult CreatePasswordHash(string password);
    bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    AuthenticationResult RefreshToken(string accessToken, string refreshToken);
    Response<User?> RevokeRefreshToken(string email);
}
```

7. Create an interface named **IUserRepository** in **Repositories** folder.
```C#
namespace AuthorizationWithJwtSample.Application.Repositories;

public interface IUserRepository
{
    User? GetById(int id);
    User? GetByEmail(string? email);
    IEnumerable<User>? GetAll();
    User? Create(
        string name,
        string email,
        byte[] passwordHash,
        byte[] passwordSalt,
        string refreshToken,
        DateTime? refreshTokenExpiryDateTime);

    User? Update(User user);
}
```

8. Create a class named **AuthenticationService** in **Services** folder and implement the **IAuthenticationService**.
```C#
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

        return new AuthenticationResult(
            accessToken,
            new RefreshToken(updatedUser.RefreshToken, updatedUser.RefreshTokenExpiryDate));

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

        var authResult = new AuthenticationResult(
            accessToken,
            new RefreshToken(createdUser.RefreshToken, createdUser.RefreshTokenExpiryDate));

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
    public AuthenticationResult RefreshToken(string accessToken, string refreshToken)
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

        var authResult = new AuthenticationResult(
            newAccessToken,
            new RefreshToken(newRefreshToken.Token, newRefreshToken.ExipryDateTime));

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
```

9. Create a class named **DependencyInjection** and register the services.
```C#
namespace AuthorizationWithJwtSample.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }
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
namespace AuthorizationWithJwtSample.Infrastructure.Authentication;

public class JwtSettings
{
    // Name of the section in appsettings.json
    public const string SectionName = "JwtSettings";

    public string Secret { get; init; } = string.Empty;
    public int ExpiryMinutes { get; init; }
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public int RefreshTokenExpiryInDays { get; set; }
}
```


6. Create a class named **JwtTokenService** in **IJwtTokenService** folder and implement the **IJwtTokenGenerator**.
```C#
namespace AuthorizationWithJwtSample.Infrastructure.Authentication;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _jwtSettings;

    // Injecting JwtSettings
    public JwtTokenService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateAccessToken(User user)
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
            new Claim(ClaimTypes.Name, user.Name),
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
            ExipryDateTime: DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryInDays));

        return refreshToken;
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            ValidateLifetime = false // Ignore token expiration date.
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;

        if (jwtSecurityToken == null ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }
}

```

7. Create a class named **UserRepository** and implement the **IUserRepository** .
```C#
namespace AuthorizationWithJwtSample.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private static List<User> _users = new List<User>();
    

    public User? Create(
        string name,
        string email,
        byte[] passwordHash,
        byte[] passwordSalt,
        string refreshToken,
        DateTime? refreshTokenExpiryDateTime)
    {
        var userId = 1;

        if (_users.Any())
        {
            userId = _users.Max(u => u.Id) + 1;
        }

        var user = new User
        {
            Id = userId,
            Name = name,
            Email = email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            RefreshToken = refreshToken,
            RefreshTokenExpiryDate = refreshTokenExpiryDateTime
        };
        _users.Add(user);

        return user;
    }

    public IEnumerable<User>? GetAll()
    {
        return _users;
    }

    public User? GetByEmail(string? email)
    {
        return _users
            .Where(u => u.Email.ToLower() == email?.ToLower())
            .SingleOrDefault();
    }

    public User? GetById(int id)
    {
        return _users
            .Where(u => u.Id == id)
            .SingleOrDefault();
    }

    public User? Update(User user)
    {
        var currentUser = _users
            .Where(u => u.Id == user.Id).SingleOrDefault();

        if (currentUser is not null)
        {
            currentUser = user;
        }

        return user;
    }
}
```

8. Create a class named **DependencyInjection** and register the services in AddInfrastructure method
```C#
namespace AuthorizationWithJwtSample.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        // Registering UserRepository
        services.AddScoped<IUserRepository, UserRepository>();

        // Binding the JwtSettings
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        // Add IOption for getting JwtSettings
        //services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton(Options.Create(jwtSettings));

        // Registering the JwtTokenGenerator
        services.AddSingleton<IJwtTokenService, JwtTokenService>();

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
                ClockSkew = TimeSpan.Zero,
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
2. In Api project open **appsettings.json** and **appsettings.Development.json** then, add JwtSettings
```json
  "JwtSettings": {
    "Secret": "my-very-very-secret-key",
    "ExpiryMinutes": 1,
    "RefreshTokenExpiryInDays": 7,
    "Issuer": "AuthorizationSample",
    "Audience": "AuthorizationSample"
  }
```
3. you can use **user-secrets** instead of setting the value in jwtSettings.
    - remove the value from **Secret**
    ```json
      "JwtSettings": {
        "Secret": "my-very-very-secret-key",
        "ExpiryMinutes": 1,
        "RefreshTokenExpiryInDays": 7,
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

4. In **Program.cs** add **UseAuthentication** and **UseAuthorization**
```C#
app.UseAuthentication();    // Adding Authentication
app.UseAuthorization();     // Adding Authorization.
```

5. In **Program.cs** add services.
```C#
builder.Services
    .AddApplication()                           // Registering Application dependencies
    .AddInfrastructure(builder.Configuration);  // Regestering Infrastructure dependencies and passing the configuration

    // Add this option to enable authentication for Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorizationm heade using the bearer scheme (bearer {token})",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
```

6. Create two record named **LoginRequest** and RegisterRequest in **Dtos** folder.
```C#
namespace AuthorizationWithJwtSample.Api.Dtos;

public record RegisterRequest(string Name, string Email, string Password);
```
```C#
namespace AuthorizationWithJwtSample.Api.Dtos;

public record LoginRequest (string Email, string Password);
```

7. Create a controller for authentication
```C#
namespace AuthorizationWithJwtSample.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("Register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        if (request is null) return BadRequest();

        var authResult = _authenticationService
            .Register(request.Name, request.Email, request.Password);

        SetRefreshTokenCookie(authResult.refreshToken);

        return Ok(authResult);
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var authResult = _authenticationService
            .Login(request.Email, request.Password);

        if (authResult is null) return Unauthorized();

        SetRefreshTokenCookie(authResult.refreshToken);

        return Ok(authResult);
    }

    [AllowAnonymous]
    [HttpPost("RefreshToken")]
    public IActionResult RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if (refreshToken is null)
        {
            return BadRequest("Invalid client request");
        }
        

        var authResult = _authenticationService
            .RefreshToken(GetAccessTokenFromHeader(), refreshToken);

        SetRefreshTokenCookie(authResult.refreshToken);

        return Ok(authResult);
    }

    [HttpPost("RevokeRefreshToken")]
    public IActionResult RevokeRefreshToken(string email)
    {
        var response = _authenticationService
            .RevokeRefreshToken(email);

        return Ok(response.Data);
    }

    private void SetRefreshTokenCookie(RefreshToken refreshToken)
    {
        var options = new CookieOptions
        {
            HttpOnly = true,
            Expires = refreshToken.ExipryDateTime
        };

        Response.Cookies.Append("refreshToken", refreshToken.Token, options);
    }
    private string GetAccessTokenFromHeader()
    {
        var keyword = "Bearer ";

        var currentAccessToken = HttpContext.Request.Headers["Authorization"]
            .ToString().Replace(keyword, string.Empty);

        return currentAccessToken;
    }
}

```