using SignalRSample.WebApi.Data;

namespace SignalRSample.WebApi.Endpoints.Orders;

public static class GetAllOrdersEndpoint
{
    public static RouteHandlerBuilder MapGetAllOrders(this RouteGroupBuilder group)
    {
        return group.MapGet("/", () =>
        {
            var orders = OrderDatabase.GetAllOrders();
            return Results.Ok(orders);
        })
        .WithName("GetAllOrders")
        .WithSummary("Get all orders")
        .WithDescription("Retrieves all orders from the database ordered by creation date");
    }
}
