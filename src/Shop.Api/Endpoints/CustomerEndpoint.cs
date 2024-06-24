using Shop.Api.Extensions;
using Shop.Api.Models.Requests;
using Shop.Api.Services;
using Carter;

namespace Shop.Api.Endpoints;

public class CustomerEndpoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapServiceEndpoints<CustomerService, CustomerRequest>("/customers", "Customer", HandleAdditionalRoutes);
    }

    public void HandleAdditionalRoutes(RouteGroupBuilder builder)
    {
        builder.MapGet("/special-route", (CustomerService service) =>
        {
            return Results.Ok("This is a special route!");
        });
        builder.MapGet("/special-test", (CustomerService service) =>
        {
            return Results.Ok("This is a special route!");
        });
    }
}
