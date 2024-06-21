using Shop.Api.Models.DTO;
using Shop.Api.Services;

namespace Shop.Api.Endpoints;

public static class OrderEndpoint
{
    public static RouteGroupBuilder MapOrderEndpoint(this WebApplication app)
    {
        return app.MapEndpoints<OrderService, OrderRequest>("/orders", "Order");
    }
}
