# Endpoint Organization Patterns

This document explains the different patterns for organizing endpoints in ASP.NET Core Minimal APIs and the approach used in this project.

## ✅ Current Approach: Extension Method with MapGroup

We're using the **Extension Method** pattern with `MapGroup` for clean, organized endpoints.

### Structure

```
src/SignalRSample.WebApi/
├── Endpoints/
│   └── OrderEndpoints.cs       ← All order-related endpoints
├── Program.cs                   ← Clean and minimal
└── ...
```

### Implementation

#### OrderEndpoints.cs
```csharp
namespace SignalRSample.WebApi.Endpoints;

public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        // Group all endpoints under /api/orders
        var group = app.MapGroup("/api/orders")
            .WithTags("Orders");

        group.MapGet("/", () => { /* ... */ })
            .WithName("GetAllOrders")
            .WithSummary("Get all orders");

        group.MapPost("/", async (request, hubContext) => { /* ... */ })
            .WithName("CreateOrder");
        
        // ... more endpoints

        return app;
    }
}
```

#### Program.cs
```csharp
using SignalRSample.WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);
// ... service configuration

var app = builder.Build();
// ... middleware configuration

// ✨ Clean and simple
app.MapOrderEndpoints();

app.Run();
```

---

## Benefits of This Approach

| Benefit | Description |
|---------|-------------|
| **Separation of Concerns** | Each feature has its own endpoint file |
| **Testability** | Endpoints can be tested independently |
| **Readability** | `Program.cs` remains clean and focused on configuration |
| **Route Grouping** | Common prefix `/api/orders` defined once |
| **Metadata** | Centralized tags, summaries, and OpenAPI docs |
| **Scalability** | Easy to add more endpoint files as the app grows |

---

## Alternative Patterns

### Pattern 1: Inline Endpoints (Original)

**❌ Not Scalable** - Everything in `Program.cs`

```csharp
// Program.cs
app.MapGet("/api/orders", () => { /* ... */ });
app.MapPost("/api/orders", async (request, context) => { /* ... */ });
app.MapPut("/api/orders/{id}/status", async (id, request, context) => { /* ... */ });
app.MapDelete("/api/orders/{id}", async (id, context) => { /* ... */ });
// ... 50 more endpoints
```

**Problems:**
- `Program.cs` becomes huge and hard to navigate
- No logical grouping
- Difficult to maintain and test

---

### Pattern 2: Multiple Extension Methods (Feature-based)

**✅ Good for Large Apps** - One file per feature

```
Endpoints/
├── OrderEndpoints.cs
├── CustomerEndpoints.cs
├── MenuEndpoints.cs
└── ReservationEndpoints.cs
```

```csharp
// Program.cs
app.MapOrderEndpoints();
app.MapCustomerEndpoints();
app.MapMenuEndpoints();
app.MapReservationEndpoints();
```

**When to use:**
- Large applications with many features
- Multiple teams working on different features
- Clear domain boundaries

---

### Pattern 3: IEndpointFilter + Endpoint Modules

**🔧 Advanced** - Using filters and modular composition

```csharp
public class OrderModule : IEndpointRouteBuilderExtensions
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/orders")
            .AddEndpointFilter<ValidationFilter>()
            .AddEndpointFilter<AuthorizationFilter>();
        
        // ... endpoints
    }
}
```

**When to use:**
- Need cross-cutting concerns (validation, auth, logging)
- Complex pipeline requirements
- Middleware-heavy applications

---

### Pattern 4: Carter Library

**📦 Third-Party** - Convention-based routing

```bash
dotnet add package Carter
```

```csharp
public class OrderModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/orders", () => { /* ... */ });
    }
}
```

```csharp
// Program.cs
builder.Services.AddCarter();
app.MapCarter();
```

**When to use:**
- Want convention-based auto-discovery
- Prefer attribute-based routing
- Large team with consistent patterns

---

## MapGroup Features

The `MapGroup` method provides powerful features for organizing endpoints:

### Common Prefix
```csharp
var group = app.MapGroup("/api/orders");

// All these are under /api/orders
group.MapGet("/", ...);              // /api/orders
group.MapGet("/{id}", ...);          // /api/orders/{id}
group.MapPost("/", ...);             // /api/orders
```

### Shared Tags (OpenAPI)
```csharp
var group = app.MapGroup("/api/orders")
    .WithTags("Orders");  // All endpoints tagged as "Orders"
```

### Common Filters
```csharp
var group = app.MapGroup("/api/orders")
    .RequireAuthorization("AdminOnly")
    .AddEndpointFilter<LoggingFilter>();
```

### Version Grouping
```csharp
var v1 = app.MapGroup("/api/v1/orders");
var v2 = app.MapGroup("/api/v2/orders");

v1.MapGet("/", GetOrdersV1);
v2.MapGet("/", GetOrdersV2);
```

---

## Real-World Example Structure

For a production app with multiple features:

```
src/YourApp.Api/
├── Endpoints/
│   ├── Orders/
│   │   ├── OrderEndpoints.cs
│   │   └── OrderFilters.cs
│   ├── Customers/
│   │   ├── CustomerEndpoints.cs
│   │   └── CustomerFilters.cs
│   └── Extensions/
│       └── EndpointExtensions.cs
├── Program.cs
└── ...
```

```csharp
// Program.cs
app.MapOrderEndpoints();
app.MapCustomerEndpoints();
app.MapMenuEndpoints();
```

---

## Testing Endpoints

With this pattern, endpoints are easy to test:

```csharp
public class OrderEndpointsTests
{
    [Fact]
    public async Task GetAllOrders_Returns_OrdersList()
    {
        // Arrange
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        // Act
        var response = await client.GetAsync("/api/orders");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
```

---

## Summary

| Pattern | Program.cs Lines | Scalability | Complexity | Best For |
|---------|------------------|-------------|------------|----------|
| **Inline** (before) | ~100+ | ❌ Low | Low | Prototypes |
| **Extension Method** (current) | ~30 | ✅ High | Medium | Production apps |
| **Multiple Extensions** | ~20 | ✅ Very High | Medium | Large apps |
| **Carter** | ~15 | ✅ Very High | High | Convention-driven |

---

## Recommended: Stick with Current Approach

For this POC and most production apps, the **Extension Method with MapGroup** pattern (what we implemented) is the sweet spot:

✅ Clean `Program.cs`  
✅ Easy to understand  
✅ Scales well  
✅ No external dependencies  
✅ Testable  
✅ Industry standard  

---

## Additional Resources

- [ASP.NET Core Minimal APIs](https://learn.microsoft.com/aspnet/core/fundamentals/minimal-apis)
- [Route Groups](https://learn.microsoft.com/aspnet/core/fundamentals/minimal-apis/route-handlers#route-groups)
- [Carter Library](https://github.com/CarterCommunity/Carter)
