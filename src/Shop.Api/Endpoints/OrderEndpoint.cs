using Shop.Api.Models.DTO;
using Shop.Api.Services;

namespace Shop.Api.Endpoints;

public static class OrderEndpoint
{
    private const string GetOrderEndpoint = "GetOrder";

    public static RouteGroupBuilder MapOrderEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/orders")
                       .WithParameterValidation()
                       .WithOpenApi()
                       .WithTags("Orders");

        group.MapGet("/", (OrderService orderService) => orderService.GetOrders());
        group.MapGet("/{id}", (int id, OrderService orderService) => orderService.GetOrderById(id)).WithName(GetOrderEndpoint);
        group.MapPost("/", (OrderRequest createOrderRequest, OrderService orderService) => orderService.CreateOrder(createOrderRequest));
        group.MapPut("/{id}", (int id, OrderRequest updateOrderRequest, OrderService orderService) => orderService.UpdateOrderById(id, updateOrderRequest));
        group.MapDelete("/{id}", (int id, OrderService orderService) => orderService.DeleteOrderById(id));

        return group;
    }
}
