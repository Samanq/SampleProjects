# Autofac Sample

Autofac is an Inversion of Control (IoC) container for .NET Core, ASP.NET Core, .NET 4.5.1+, Universal Windows apps, and more.

---

## Setting-up
1. Install **Autofac.Extensions.DependencyInjection** Package.
2. Create and interface and a class as a sample service.
```C#
namespace AutofacSample.Api.Services.Interfaces;

public interface IEmailService
{
    string ReadEmail();
    bool SendEmail(string body);
}

```
```C#
using AutofacSample.Api.Services.Interfaces;

namespace AutofacSample.Api.Services;

public class EmailService : IEmailService
{
    public string ReadEmail()
    {
        return "This is a new email";
    }

    public bool SendEmail(string body)
    {
        return true;
    }
}

```
3. Create a **controller** and inject the **IEmailService** we created before.
```C#
using AutofacSample.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutofacSample.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailsController : ControllerBase
{
    private readonly IEmailService _emailService;

    // Injecting IEmailService
    public EmailsController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpGet]
    public IActionResult GetEmail()
    {
        return Ok(_emailService.ReadEmail());
    }

    [HttpPost]
    public IActionResult Post(string text)
    {
        return Ok(_emailService.SendEmail(text));
    }
}

```
4. Open **Program.cs** add Register the service.
```C#
var builder = WebApplication.CreateBuilder(args);

// Adding the AutofacModule
builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        // Registering a single Service.
        builder.RegisterType<EmailService>().As<IEmailService>();
    });
```

4.1. you can also register a module here. for doing this first you must create a class and inherit from **Autofac.Module** 
```C#
using Autofac;
using AutofacSample.Api.Services;
using AutofacSample.Api.Services.Interfaces;

namespace AutofacSample.Api.Autofac;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // Registering a service
        builder.RegisterType<EmailService>().As<IEmailService>();
    }
}
```
4.2. Now you can open **Program.cs** and register the module.
```C#
// Adding the AutofacModule
builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        // Registering a module.
        builder.RegisterModule(new AutofacModule());
    });
```
---

## Registering all services in a assembly
[Autofac documentation](https://autofac.readthedocs.io/en/latest/register/scanning.html)
```C#
var infrastructureAssembly = System.Reflection.Assembly.Load("AutofacSample.Infrastructure");

builder.RegisterAssemblyTypes(infrastructureAssembly)
    .Where(t => t.Name.EndsWith("Service"))
    .AsImplementedInterfaces();
```
---

## Instance Scope
[Autofac documentation](https://autofac.readthedocs.io/en/latest/lifetime/instance-scope.html)
### **Default or Instance Per Dependency**
Also called **‘transient’** or **‘factory’** in other containers. Using per-dependency scope, a unique instance will be returned from each request for a service.
```C#
builder.RegisterType<EmailService>().As<IEmailService>();
        
// OR
builder.RegisterType<EmailService>().As<IEmailService>()
    .InstancePerDependency();
```

### **Single Instance**
This is also known as **‘singleton’** Using single instance scope, one instance is returned from all requests in the root and all nested scopes.
```C#
builder.RegisterType<EmailService>()
    .As<IEmailService>()
    .SingleInstance();
```

### **Instance Per Lifetime Scope**
This scope applies to nested lifetimes. A component with per-lifetime scope will have at most a single instance per nested lifetime scope.

This is useful for objects specific to a **single unit of work** that may need to nest **additional logical units of work**. Each nested lifetime scope will get a new instance of the registered dependency.
```C#
builder.RegisterType<EmailService>()
    .As<IEmailService>()
    .InstancePerLifetimeScope();
```

### **Instance Per Request**
Some application types naturally lend themselves to “request” type semantics, for example ASP.NET web forms and MVC applications. In these application types, it’s helpful to have the ability to have a sort of “singleton per request.”

```C#
builder.RegisterType<EmailService>()
    .As<IEmailService>()
    .InstancePerRequest();
```
There are more Instances wich you can find in [Autofac documentation](https://autofac.readthedocs.io/en/latest/lifetime/instance-scope.html).

---

