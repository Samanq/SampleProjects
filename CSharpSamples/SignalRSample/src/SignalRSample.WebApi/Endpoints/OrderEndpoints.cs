using SignalRSample.WebApi.Endpoints.Orders;

namespace SignalRSample.WebApi.Endpoints;

/// <summary>
/// Composes all order-related endpoints into a single route group
/// </summary>
public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app
            .MapGroup("/api/orders")
            .WithTags("Orders");

        // Map all individual endpoints
        group.MapGetAllOrders();
        group.MapGetOrderById();
        group.MapCreateOrder();
        group.MapUpdateOrderStatus();
        group.MapDeleteOrder();
        group.MapGetOrderStatuses();

        return app;
    }
}