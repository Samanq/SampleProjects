# SignalR Restaurant Order Management Sample

A real-time restaurant order management system demonstrating **SignalR** integration between an **ASP.NET Core Web API** backend and a **Blazor WebAssembly** frontend.

## 🍽️ Overview

This proof-of-concept application simulates a restaurant order system where:
- Staff can create new orders with multiple items
- Order status updates are broadcast in real-time to all connected clients
- Multiple browser windows/tabs stay synchronized automatically

## 📁 Project Structure

```
SignalRSample/
├── SignalRSample.slnx
└── src/
    ├── SingalRSample.WebApi/          # ASP.NET Core Web API + SignalR Hub
    │   ├── Data/
    │   │   └── OrderDatabase.cs       # In-memory static database
    │   ├── DTOs/
    │   │   └── CreateOrderRequest.cs  # Request DTOs
    │   ├── Hubs/
    │   │   └── OrderHub.cs            # SignalR Hub
    │   ├── Models/
    │   │   └── Order.cs               # Domain models
    │   └── Program.cs                 # API endpoints & configuration
    │
    └── SignalRSample.BlazorWasm/      # Blazor WebAssembly Client
        ├── Models/
        │   └── Order.cs               # Client-side models
        ├── Pages/
        │   └── Orders.razor           # Orders management page
        └── Layout/
            └── NavMenu.razor          # Navigation menu
```

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download) or later
- Your favorite IDE (VS Code, Visual Studio, Rider)

### Running the Application

1. **Clone the repository** and navigate to the project folder:
   ```bash
   cd SignalRSample
   ```

2. **Start the Web API** (Terminal 1):
   ```bash
   cd src/SingalRSample.WebApi
   dotnet run
   ```
   The API will start at `http://localhost:5101`

3. **Start the Blazor WASM Client** (Terminal 2):
   ```bash
   cd src/SignalRSample.BlazorWasm
   dotnet run
   ```
   The client will start at `http://localhost:5024`

4. **Open the application** in your browser:
   - Navigate to `http://localhost:5024/orders`
   - Open multiple browser tabs to see real-time synchronization!

---

## 📚 Step-by-Step Tutorial

This tutorial walks through how SignalR was set up in this project.

### Part 1: Setting Up the Web API (Backend)

#### Step 1.1: Create the Web API Project

```bash
dotnet new webapi -n SingalRSample.WebApi
```

#### Step 1.2: Create the Domain Models

Create `Models/Order.cs`:

```csharp
namespace SingalRSample.WebApi.Models;

public class Order
{
    public int Id { get; set; }
    public string TableNumber { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public List<OrderItem> Items { get; set; } = new();
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public decimal TotalAmount => Items.Sum(i => i.Price * i.Quantity);
}

public class OrderItem
{
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public enum OrderStatus
{
    Pending = 0,
    Confirmed = 1,
    Preparing = 2,
    Ready = 3,
    Served = 4,
    Cancelled = 5
}
```

#### Step 1.3: Create the In-Memory Database

Create `Data/OrderDatabase.cs`:

```csharp
using SingalRSample.WebApi.Models;

namespace SingalRSample.WebApi.Data;

public static class OrderDatabase
{
    private static readonly object _lock = new();
    private static int _nextId = 1;
    private static readonly List<Order> _orders = new();

    public static List<Order> GetAllOrders()
    {
        lock (_lock)
        {
            return _orders.OrderByDescending(o => o.CreatedAt).ToList();
        }
    }

    public static Order? GetOrderById(int id)
    {
        lock (_lock)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }
    }

    public static Order AddOrder(Order order)
    {
        lock (_lock)
        {
            order.Id = _nextId++;
            order.CreatedAt = DateTime.UtcNow;
            order.Status = OrderStatus.Pending;
            _orders.Add(order);
            return order;
        }
    }

    public static Order? UpdateOrderStatus(int id, OrderStatus newStatus)
    {
        lock (_lock)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                order.Status = newStatus;
                order.UpdatedAt = DateTime.UtcNow;
            }
            return order;
        }
    }

    public static bool DeleteOrder(int id)
    {
        lock (_lock)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                _orders.Remove(order);
                return true;
            }
            return false;
        }
    }
}
```

#### Step 1.4: Create the SignalR Hub

Create `Hubs/OrderHub.cs`:

```csharp
using Microsoft.AspNetCore.SignalR;

namespace SingalRSample.WebApi.Hubs;

public class OrderHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"Client connected: {Context.ConnectionId}");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine($"Client disconnected: {Context.ConnectionId}");
        await base.OnDisconnectedAsync(exception);
    }
}
```

> **Note:** The hub itself doesn't need methods for our use case. We'll use `IHubContext<OrderHub>` from API endpoints to broadcast messages.

#### Step 1.5: Configure SignalR in Program.cs

Update `Program.cs`:

```csharp
using Microsoft.AspNetCore.SignalR;
using SingalRSample.WebApi.Data;
using SingalRSample.WebApi.Hubs;
using SingalRSample.WebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Add SignalR services
builder.Services.AddSignalR();

// 2. Add CORS for Blazor WASM client
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorClient", policy =>
    {
        policy.WithOrigins("http://localhost:5024", "https://localhost:7085")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();  // Required for SignalR
    });
});

var app = builder.Build();

// 3. Use CORS
app.UseCors("BlazorClient");

// 4. Map the SignalR Hub endpoint
app.MapHub<OrderHub>("/hubs/orders");

// ... API endpoints
app.Run();
```

#### Step 1.6: Create API Endpoints with SignalR Broadcasting

Add API endpoints that broadcast changes via SignalR:

```csharp
// Create new order - broadcasts to all clients
app.MapPost("/api/orders", async (CreateOrderRequest request, IHubContext<OrderHub> hubContext) =>
{
    var order = new Order
    {
        TableNumber = request.TableNumber,
        CustomerName = request.CustomerName,
        Items = request.Items
    };
    
    var createdOrder = OrderDatabase.AddOrder(order);
    
    // 🔔 Broadcast new order to all connected clients
    await hubContext.Clients.All.SendAsync("ReceiveNewOrder", createdOrder);
    
    return Results.Created($"/api/orders/{createdOrder.Id}", createdOrder);
});

// Update order status - broadcasts to all clients
app.MapPut("/api/orders/{id}/status", async (int id, UpdateOrderStatusRequest request, IHubContext<OrderHub> hubContext) =>
{
    var updatedOrder = OrderDatabase.UpdateOrderStatus(id, request.Status);
    
    if (updatedOrder is null)
        return Results.NotFound();
    
    // 🔔 Broadcast status change to all connected clients
    await hubContext.Clients.All.SendAsync("ReceiveOrderStatusChanged", updatedOrder);
    
    return Results.Ok(updatedOrder);
});

// Delete order - broadcasts to all clients
app.MapDelete("/api/orders/{id}", async (int id, IHubContext<OrderHub> hubContext) =>
{
    var deleted = OrderDatabase.DeleteOrder(id);
    
    if (!deleted)
        return Results.NotFound();
    
    // 🔔 Broadcast deletion to all connected clients
    await hubContext.Clients.All.SendAsync("ReceiveOrderDeleted", id);
    
    return Results.NoContent();
});
```

---

### Part 2: Setting Up Blazor WebAssembly (Frontend)

#### Step 2.1: Create the Blazor WASM Project

```bash
dotnet new blazorwasm -n SignalRSample.BlazorWasm
```

#### Step 2.2: Add SignalR Client Package

```bash
cd SignalRSample.BlazorWasm
dotnet add package Microsoft.AspNetCore.SignalR.Client
```

Or add to `.csproj`:

```xml
<PackageReference Include="Microsoft.AspNetCore.SignalR.Client" Version="10.0.0"/>
```

#### Step 2.3: Create Client-Side Models

Create `Models/Order.cs` (mirror the server models):

```csharp
namespace SignalRSample.BlazorWasm.Models;

public class Order
{
    public int Id { get; set; }
    public string TableNumber { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public List<OrderItem> Items { get; set; } = new();
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public decimal TotalAmount { get; set; }
}

public class OrderItem
{
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public enum OrderStatus
{
    Pending = 0,
    Confirmed = 1,
    Preparing = 2,
    Ready = 3,
    Served = 4,
    Cancelled = 5
}
```

#### Step 2.4: Create the Orders Page with SignalR Connection

Create `Pages/Orders.razor`:

```razor
@page "/orders"
@using Microsoft.AspNetCore.SignalR.Client
@using SignalRSample.BlazorWasm.Models
@inject HttpClient Http
@implements IAsyncDisposable

<h1>🍽️ Restaurant Orders</h1>

<!-- Connection Status Indicator -->
<div class="mb-3">
    <span class="badge @(IsConnected ? "bg-success" : "bg-danger")">
        @(IsConnected ? "🟢 Connected" : "🔴 Disconnected")
    </span>
</div>

<!-- Orders Table -->
@foreach (var order in orders)
{
    <div>Order #@order.Id - @order.Status</div>
}

@code {
    private HubConnection? hubConnection;
    private List<Order> orders = new();
    private const string ApiBaseUrl = "http://localhost:5101";

    protected override async Task OnInitializedAsync()
    {
        // 1. Load initial data via HTTP
        await LoadOrders();
        
        // 2. Start SignalR connection
        await StartSignalRConnection();
    }

    private async Task StartSignalRConnection()
    {
        // 3. Build the hub connection
        hubConnection = new HubConnectionBuilder()
            .WithUrl($"{ApiBaseUrl}/hubs/orders")
            .WithAutomaticReconnect()  // Auto-reconnect on connection loss
            .Build();

        // 4. Register event handlers for server messages
        
        // Handle new order
        hubConnection.On<Order>("ReceiveNewOrder", (order) =>
        {
            if (!orders.Any(o => o.Id == order.Id))
            {
                orders.Insert(0, order);
                InvokeAsync(StateHasChanged);  // Update UI
            }
        });

        // Handle order status change
        hubConnection.On<Order>("ReceiveOrderStatusChanged", (updatedOrder) =>
        {
            var existing = orders.FirstOrDefault(o => o.Id == updatedOrder.Id);
            if (existing != null)
            {
                var index = orders.IndexOf(existing);
                orders[index] = updatedOrder;
                InvokeAsync(StateHasChanged);
            }
        });

        // Handle order deletion
        hubConnection.On<int>("ReceiveOrderDeleted", (orderId) =>
        {
            var order = orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                orders.Remove(order);
                InvokeAsync(StateHasChanged);
            }
        });

        // 5. Start the connection
        await hubConnection.StartAsync();
    }

    private async Task LoadOrders()
    {
        orders = await Http.GetFromJsonAsync<List<Order>>($"{ApiBaseUrl}/api/orders") ?? new();
    }

    // 6. Check connection state
    private bool IsConnected => hubConnection?.State == HubConnectionState.Connected;

    // 7. Cleanup on dispose
    public async ValueTask DisposeAsync()
    {
        if (hubConnection is not null)
        {
            await hubConnection.DisposeAsync();
        }
    }
}
```

---

### Part 3: Key SignalR Concepts Explained

#### 3.1 Hub Connection Lifecycle

```
Client                          Server (Hub)
  |                                  |
  |------ StartAsync() ------------>|  OnConnectedAsync()
  |                                  |
  |<----- SendAsync("Method") ------|  Broadcast to clients
  |                                  |
  |------ InvokeAsync("Method") --->|  Call hub method
  |                                  |
  |------ DisposeAsync() ---------->|  OnDisconnectedAsync()
  |                                  |
```

#### 3.2 Broadcasting Patterns

| Pattern | Code | Description |
|---------|------|-------------|
| All Clients | `Clients.All.SendAsync(...)` | Send to everyone |
| Specific Client | `Clients.Client(connectionId).SendAsync(...)` | Send to one client |
| Groups | `Clients.Group("groupName").SendAsync(...)` | Send to a group |
| Caller Only | `Clients.Caller.SendAsync(...)` | Send back to caller |
| Others | `Clients.Others.SendAsync(...)` | Everyone except caller |

#### 3.3 Using IHubContext Outside the Hub

Inject `IHubContext<THub>` to broadcast from controllers or endpoints:

```csharp
app.MapPost("/api/orders", async (
    CreateOrderRequest request, 
    IHubContext<OrderHub> hubContext) =>  // Inject hub context
{
    // ... create order ...
    
    // Broadcast from outside the hub
    await hubContext.Clients.All.SendAsync("ReceiveNewOrder", order);
});
```

---

### Part 4: Testing Real-Time Updates

1. **Open two browser windows** side by side, both at `http://localhost:5024/orders`

2. **Create an order** in one window → Watch it appear instantly in the other!

3. **Update order status** → Both windows update simultaneously

4. **Delete an order** → Removed from all connected clients

---

## 🔧 Configuration

### API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/orders` | Get all orders |
| GET | `/api/orders/{id}` | Get order by ID |
| POST | `/api/orders` | Create new order |
| PUT | `/api/orders/{id}/status` | Update order status |
| DELETE | `/api/orders/{id}` | Delete order |
| GET | `/api/orders/statuses` | Get available statuses |

### SignalR Events

| Event Name | Payload | Description |
|------------|---------|-------------|
| `ReceiveNewOrder` | `Order` | New order created |
| `ReceiveOrderStatusChanged` | `Order` | Order status updated |
| `ReceiveOrderDeleted` | `int` (orderId) | Order deleted |

### Ports

| Service | URL |
|---------|-----|
| Web API | `http://localhost:5101` |
| SignalR Hub | `http://localhost:5101/hubs/orders` |
| Blazor WASM | `http://localhost:5024` |

---

## 🎯 Order Status Workflow

```
┌─────────┐    ┌───────────┐    ┌───────────┐    ┌───────┐    ┌────────┐
│ Pending │ -> │ Confirmed │ -> │ Preparing │ -> │ Ready │ -> │ Served │
└─────────┘    └───────────┘    └───────────┘    └───────┘    └────────┘
     │
     └──────────────────────────────────────────────────────> ┌───────────┐
                                                              │ Cancelled │
                                                              └───────────┘
```

---

## 📖 Additional Resources

- [ASP.NET Core SignalR Documentation](https://docs.microsoft.com/aspnet/core/signalr/)
- [Blazor WebAssembly Documentation](https://docs.microsoft.com/aspnet/core/blazor/)
- [SignalR Client in .NET](https://docs.microsoft.com/aspnet/core/signalr/dotnet-client)

---

## 📝 License

This sample is provided for educational purposes.
