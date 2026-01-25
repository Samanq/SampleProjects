using SignalRSample.WebApi.Models;

namespace SignalRSample.WebApi.DTOs;

public class CreateOrderRequest
{
    public string TableNumber { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public List<OrderItem> Items { get; set; } = new();
}

public class UpdateOrderStatusRequest
{
    public OrderStatus Status { get; set; }
}
