using Microsoft.AspNetCore.SignalR;
using SignalRSample.WebApi.Data;
using SignalRSample.WebApi.DTOs;
using SignalRSample.WebApi.Hubs;
using SignalRSample.WebApi.Models;

namespace SignalRSample.WebApi.Endpoints;

public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/orders")
            .WithTags("Orders");

        // Get all orders
        group.MapGet("/", () =>
        {
            var orders = OrderDatabase.GetAllOrders();
            return Results.Ok(orders);
        })
        .WithName("GetAllOrders")
        .WithSummary("Get all orders")
        .WithDescription("Retrieves all orders from the database ordered by creation date");

        // Get order by ID
        group.MapGet("/{id}", (int id) =>
        {
            var order = OrderDatabase.GetOrderById(id);
            return order is not null ? Results.Ok(order) : Results.NotFound();
        })
        .WithName("GetOrderById")
        .WithSummary("Get order by ID")
        .WithDescription("Retrieves a specific order by its ID");

        // Create new order
        group.MapPost("/", async (CreateOrderRequest request, IHubContext<OrderHub> hubContext) =>
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
        .WithName("CreateOrder")
        .WithSummary("Create a new order")
        .WithDescription("Creates a new order and broadcasts it to all connected clients via SignalR");

        // Update order status
        group.MapPut("/{id}/status", async (int id, UpdateOrderStatusRequest request, IHubContext<OrderHub> hubContext) =>
        {
            var updatedOrder = OrderDatabase.UpdateOrderStatus(id, request.Status);
            
            if (updatedOrder is null)
                return Results.NotFound();
            
            // Notify all clients about the status change via SignalR
            await hubContext.Clients.All.SendAsync("ReceiveOrderStatusChanged", updatedOrder);
            
            return Results.Ok(updatedOrder);
        })
        .WithName("UpdateOrderStatus")
        .WithSummary("Update order status")
        .WithDescription("Updates the status of an order and broadcasts the change to all connected clients");

        // Delete order
        group.MapDelete("/{id}", async (int id, IHubContext<OrderHub> hubContext) =>
        {
            var deleted = OrderDatabase.DeleteOrder(id);
            
            if (!deleted)
                return Results.NotFound();
            
            // Notify all clients about the deletion via SignalR
            await hubContext.Clients.All.SendAsync("ReceiveOrderDeleted", id);
            
            return Results.NoContent();
        })
        .WithName("DeleteOrder")
        .WithSummary("Delete an order")
        .WithDescription("Deletes an order and notifies all connected clients");

        // Get available order statuses (for UI dropdown)
        group.MapGet("/statuses", () =>
        {
            var statuses = Enum.GetValues<OrderStatus>()
                .Select(s => new { Value = (int)s, Name = s.ToString() });
            return Results.Ok(statuses);
        })
        .WithName("GetOrderStatuses")
        .WithSummary("Get order statuses")
        .WithDescription("Retrieves all available order status values");

        return app;
    }
}
