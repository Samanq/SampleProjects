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

1. Create contracts in Peresntation.Contracts
```c#
namespace CleanArchitecture.Contracts.Authentication;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password);
```
