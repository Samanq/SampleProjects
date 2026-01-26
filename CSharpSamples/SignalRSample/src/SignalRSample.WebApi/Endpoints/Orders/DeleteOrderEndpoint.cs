using Microsoft.AspNetCore.SignalR;
using SignalRSample.WebApi.Data;
using SignalRSample.WebApi.Hubs;

namespace SignalRSample.WebApi.Endpoints.Orders;

public static class DeleteOrderEndpoint
{
    public static RouteHandlerBuilder MapDeleteOrder(this RouteGroupBuilder group)
    {
        return group.MapDelete("/{id}", async (int id, IHubContext<OrderHub> hubContext) =>
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
    }
}
