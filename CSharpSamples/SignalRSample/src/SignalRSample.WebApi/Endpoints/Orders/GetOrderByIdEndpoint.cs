using SignalRSample.WebApi.Data;

namespace SignalRSample.WebApi.Endpoints.Orders;

public static class GetOrderByIdEndpoint
{
    public static RouteHandlerBuilder MapGetOrderById(this RouteGroupBuilder group)
    {
        return group.MapGet("/{id}", (int id) =>
        {
            var order = OrderDatabase.GetOrderById(id);
            return order is not null ? Results.Ok(order) : Results.NotFound();
        })
        .WithName("GetOrderById")
        .WithSummary("Get order by ID")
        .WithDescription("Retrieves a specific order by its ID");
    }
}
