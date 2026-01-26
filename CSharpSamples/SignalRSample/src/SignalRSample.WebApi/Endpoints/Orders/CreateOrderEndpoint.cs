using Microsoft.AspNetCore.SignalR;
using SignalRSample.WebApi.Data;
using SignalRSample.WebApi.DTOs;
using SignalRSample.WebApi.Hubs;
using SignalRSample.WebApi.Models;

namespace SignalRSample.WebApi.Endpoints.Orders;

public static class CreateOrderEndpoint
{
    public static RouteHandlerBuilder MapCreateOrder(this RouteGroupBuilder group)
    {
        return group.MapPost("/", async (CreateOrderRequest request, IHubContext<OrderHub> hubContext) =>
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
    }
}
