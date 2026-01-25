using Microsoft.AspNetCore.SignalR;
using SingalRSample.WebApi.Data;
using SingalRSample.WebApi.DTOs;
using SingalRSample.WebApi.Hubs;
using SingalRSample.WebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

// Add SignalR
builder.Services.AddSignalR();

// Add CORS for Blazor WASM client
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorClient", policy =>
    {
        policy.WithOrigins("http://localhost:5024", "https://localhost:7085")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("BlazorClient");

// Map SignalR Hub
app.MapHub<OrderHub>("/hubs/orders");

// ==================== ORDER API ENDPOINTS ====================

// Get all orders
app.MapGet("/api/orders", () =>
{
    var orders = OrderDatabase.GetAllOrders();
    return Results.Ok(orders);
})
.WithName("GetAllOrders");

// Get order by ID
app.MapGet("/api/orders/{id}", (int id) =>
{
    var order = OrderDatabase.GetOrderById(id);
    return order is not null ? Results.Ok(order) : Results.NotFound();
})
.WithName("GetOrderById");

// Create new order
app.MapPost("/api/orders", async (CreateOrderRequest request, IHubContext<OrderHub> hubContext) =>
{
    var order = new Order
    {
        TableNumber = request.TableNumber,
        CustomerName = request.CustomerName,
        Items = request.Items
    };
    
    var createdOrder = OrderDatabase.AddOrder(order);
    
    // Notify all clients about the new order via SignalR
    await hubContext.Clients.All.SendAsync("ReceiveNewOrder", createdOrder);
    
    return Results.Created($"/api/orders/{createdOrder.Id}", createdOrder);
})
.WithName("CreateOrder");

// Update order status
app.MapPut("/api/orders/{id}/status", async (int id, UpdateOrderStatusRequest request, IHubContext<OrderHub> hubContext) =>
{
    var updatedOrder = OrderDatabase.UpdateOrderStatus(id, request.Status);
    
    if (updatedOrder is null)
        return Results.NotFound();
    
    // Notify all clients about the status change via SignalR
    await hubContext.Clients.All.SendAsync("ReceiveOrderStatusChanged", updatedOrder);
    
    return Results.Ok(updatedOrder);
})
.WithName("UpdateOrderStatus");

// Delete order
app.MapDelete("/api/orders/{id}", async (int id, IHubContext<OrderHub> hubContext) =>
{
    var deleted = OrderDatabase.DeleteOrder(id);
    
    if (!deleted)
        return Results.NotFound();
    
    // Notify all clients about the deletion via SignalR
    await hubContext.Clients.All.SendAsync("ReceiveOrderDeleted", id);
    
    return Results.NoContent();
})
.WithName("DeleteOrder");

// Get available order statuses (for UI dropdown)
app.MapGet("/api/orders/statuses", () =>
{
    var statuses = Enum.GetValues<OrderStatus>()
        .Select(s => new { Value = (int)s, Name = s.ToString() });
    return Results.Ok(statuses);
})
.WithName("GetOrderStatuses");

app.Run();