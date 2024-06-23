using Carter;
using Shop.Api.Extensions;
using Shop.Api.Models.Requests;
using Shop.Api.Services;

namespace Shop.Api.Endpoints;

public class OrderEndpoint : CarterModule
{

    public override void AddRoutes(IEndpointRouteBuilder builder)
    {
        builder.MapServiceEndpoints<OrderService, OrderRequest>("/orders", "Order", HandleAdditionalRoutes);
    }
    public void HandleAdditionalRoutes(RouteGroupBuilder builder)
    {
        builder.MapGet("/special-route", (CustomerService service) =>
        {
            return Results.Ok("This is a special route!");
        });
        builder.MapGet("/special-routes", (CustomerService service) =>
        {
            return Results.Ok("This is a special route!");
        });
    }
}
