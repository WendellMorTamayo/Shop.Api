using Carter;
using Shop.Api.Extensions;
using Shop.Api.Models.Requests;
using Shop.Api.Services;

namespace Shop.Api.Endpoints;

public class ProductEndpoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder builder)
    {
        builder.MapServiceEndpoints<ProductService, ProductRequest>("/products", "Product", HandleAdditionalRoutes);
    }
    public void HandleAdditionalRoutes(RouteGroupBuilder builder)
    {
        builder.MapGet("/test", (ProductService service) => service.GetAll());
        builder.MapGet("/test-route", (ProductService service) =>
        {
            return Results.Ok("This is a test route!");
        });
    }
}
