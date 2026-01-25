using SignalRSample.WebApi.Models;

namespace SignalRSample.WebApi.Data;

/// <summary>
/// Static in-memory database for orders (POC purposes)
/// </summary>
public static class OrderDatabase
{
    private static readonly object _lock = new();
    private static int _nextId = 1;
    
    private static readonly List<Order> _orders = new()
    {
        new Order
        {
            Id = _nextId++,
            TableNumber = "T1",
            CustomerName = "John Doe",
            Status = OrderStatus.Preparing,
            CreatedAt = DateTime.UtcNow.AddMinutes(-15),
            Items = new List<OrderItem>
            {
                new() { Name = "Margherita Pizza", Quantity = 1, Price = 12.99m },
                new() { Name = "Caesar Salad", Quantity = 1, Price = 8.99m },
                new() { Name = "Coke", Quantity = 2, Price = 2.50m }
            }
        },
        new Order
        {
            Id = _nextId++,
            TableNumber = "T3",
            CustomerName = "Jane Smith",
            Status = OrderStatus.Pending,
            CreatedAt = DateTime.UtcNow.AddMinutes(-5),
            Items = new List<OrderItem>
            {
                new() { Name = "Spaghetti Carbonara", Quantity = 2, Price = 14.99m },
                new() { Name = "Garlic Bread", Quantity = 1, Price = 4.99m }
            }
        },
        new Order
        {
            Id = _nextId++,
            TableNumber = "T5",
            CustomerName = "Bob Wilson",
            Status = OrderStatus.Ready,
            CreatedAt = DateTime.UtcNow.AddMinutes(-25),
            Items = new List<OrderItem>
            {
                new() { Name = "Grilled Salmon", Quantity = 1, Price = 22.99m },
                new() { Name = "House Wine", Quantity = 1, Price = 8.00m }
            }
        }
    };

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
