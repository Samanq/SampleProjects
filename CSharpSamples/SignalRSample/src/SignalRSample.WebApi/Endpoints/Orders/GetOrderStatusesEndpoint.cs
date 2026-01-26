using SignalRSample.WebApi.Models;

namespace SignalRSample.WebApi.Endpoints.Orders;

public static class GetOrderStatusesEndpoint
{
    public static RouteHandlerBuilder MapGetOrderStatuses(this RouteGroupBuilder group)
    {
        return group.MapGet("/statuses", () =>
        {
            var statuses = Enum.GetValues<OrderStatus>()
                .Select(s => new { Value = (int)s, Name = s.ToString() });
            return Results.Ok(statuses);
        })
        .WithName("GetOrderStatuses")
        .WithSummary("Get order statuses")
        .WithDescription("Retrieves all available order status values");
    }
}
