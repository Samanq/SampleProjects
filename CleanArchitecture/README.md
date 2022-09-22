# Clean Architecture

## Setup the folders structure
1. Create four layers.
2. Create a API project named CleanArchitecture.Api in Presentation folder.
3. Create a class library project named CleanArchitecture.Contracts in Presentation folder.
4. Create a class library project named CleanArchitecture.Application in Application folder.
5. Create a class library project named CleanArchitecture.Infrastructure in Infrastructure folder.
6. Create a class library project named CleanArchitecture.Domain in Domain folder.

## Projects refrences 

1. In the Api project add a refrence to conctracts and application projects.
2. In the Infrastructure project add a refrence to the application project.
3. In the Application project add a refrence to the domain project.
4. In the Api project add a refrence to the Infrastructure project. (theoretically we shouldn't have a refrence from Presentation layer to Infrastructure layer, but in actuality we need a refrence to infrastructure  )
---
## Contracts

1. Create contracts in Peresntation.Contracts.
```c#
namespace CleanArchitecture.Contracts.Authentication;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password);
```

## API
1. Create the Api Controllers in Api projects.
```C#
namespace CleanArchitecture.Api.Controllers;

using CleanArchitecture.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody]LoginRequest request)
    {
        return NoContent();
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        return NoContent();
    }
}

```
## Application layer
We Defining our Interface here.
1. Install Microsoft.Extensions.DependencyInjection.Abstractions package
2. Create services here
3. Define the DependencyInjection

## Infrastucture layer
We Implenet the Interfaces here
1. Install Microsoft.Extensions.DependencyInjection.Abstractions package
2. Define the DependencyInjection
```C#
namespace CleanArchitecture.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraStructure(this IServiceCollection services)
    {
        return services;
    }
}
```
3. Install System.IdentityModel.Tokens.Jwt package
4. Install Microsoft.Extensions.Configuration package  
5. Install Microsoft.Extensions.Options.ConfigurationExtensions package

### Adding user secret
1. For initialing the user secrets run this command in terminal
    - dotnet user-secrets init --project \<projectFolderPath>
2. For setting the value of the user secret run this command in terminal
    - dotnet user-secret set --project \<projectFolderPath> \<"settingKeyNameInAppSetting"> \<"user-secret-value">

Example
```powershell
dotnet user-secrets init --project .\Presentation\CleanArchitecture.Api\
dotnet user-secrets set --project .\Presentation\CleanArchitecture.Api\ "JwtSettings:Secret" "super-secret-key-from-user-secrets"
```
