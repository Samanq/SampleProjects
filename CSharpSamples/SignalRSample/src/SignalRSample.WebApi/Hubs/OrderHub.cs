using Microsoft.AspNetCore.SignalR;
using SignalRSample.WebApi.Models;

namespace SignalRSample.WebApi.Hubs;

/// <summary>
/// SignalR Hub for real-time order updates
/// </summary>
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

    /// <summary>
    /// Broadcast new order to all connected clients
    /// </summary>
    public async Task NotifyNewOrder(Order order)
    {
        await Clients.All.SendAsync("ReceiveNewOrder", order);
    }
    
    /// <summary>
    /// Broadcast order status update to all connected clients
    /// </summary>
    public async Task NotifyOrderStatusChanged(Order order)
    {
        await Clients.All.SendAsync("ReceiveOrderStatusChanged", order);
    }

    /// <summary>
    /// Broadcast order deletion to all connected clients
    /// </summary>
    public async Task NotifyOrderDeleted(int orderId)
    {
        await Clients.All.SendAsync("ReceiveOrderDeleted", orderId);
    }
}
