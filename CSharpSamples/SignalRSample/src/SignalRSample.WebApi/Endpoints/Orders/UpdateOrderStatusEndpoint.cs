using Microsoft.AspNetCore.SignalR;
using SignalRSample.WebApi.Data;
using SignalRSample.WebApi.DTOs;
using SignalRSample.WebApi.Hubs;

namespace SignalRSample.WebApi.Endpoints.Orders;

public static class UpdateOrderStatusEndpoint
{
    public static RouteHandlerBuilder MapUpdateOrderStatus(this RouteGroupBuilder group)
    {
        return group.MapPut("/{id}/status",
                async (int id, UpdateOrderStatusRequest request, IHubContext<OrderHub> hubContext) =>
        {
            var updatedOrder = OrderDatabase.UpdateOrderStatus(id, request.Status);

            if (updatedOrder is null)
            {
                return Results.NotFound();
            }
            
            // Notify all clients about the status change via SignalR
            await hubContext.Clients.All.SendAsync("ReceiveOrderStatusChanged", updatedOrder);
            
            return Results.Ok(updatedOrder);
        })
        .WithName("UpdateOrderStatus")
        .WithSummary("Update order status")
        .WithDescription("Updates the status of an order and broadcasts the change to all connected clients");
    }
}
