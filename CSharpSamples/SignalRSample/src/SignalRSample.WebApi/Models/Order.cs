namespace SignalRSample.WebApi.Models;

public class Order
{
    public int Id { get; set; }
    public string TableNumber { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public List<OrderItem> Items { get; init; } = [];
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public decimal TotalAmount => Items.Sum(i => i.Price * i.Quantity);
}

public class OrderItem
{
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; init; }
    public decimal Price { get; init; }
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
