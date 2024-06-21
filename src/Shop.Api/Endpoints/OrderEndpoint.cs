using Shop.Api.Models.DTO;
using Shop.Api.Services;

namespace Shop.Api.Endpoints;

public class OrderEndpoint : IEndpointModule
{
    public void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/orders")
                       .WithParameterValidation()
                       .WithOpenApi()
                       .WithTags("Orders");

        group.MapGet("/", (OrderService orderService) => orderService.GetOrders());
        group.MapGet("/{id}", (int id, OrderService orderService) => orderService.GetOrderById(id)).WithName(OrderService.GetOrderEndpoint);
        group.MapPost("/", (OrderRequest createOrderRequest, OrderService orderService) => orderService.CreateOrder(createOrderRequest));
        group.MapPut("/{id}", (int id, OrderRequest updateOrderRequest, OrderService orderService) => orderService.UpdateOrderById(id, updateOrderRequest));
        group.MapDelete("/{id}", (int id, OrderService orderService) => orderService.DeleteOrderById(id));
    }
}
