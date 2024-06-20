using Shop.Api.Models;
using Shop.Api.Models.DTO;
using Shop.Api.Data;
using System.Linq;

namespace Shop.Api.Endpoints;
public static class OrderEndpoint
{
    const string GetOrderEndpoint = "GetOrder";

    private static List<Order> GetOrders()
    {
        return DataStore.Orders;
    }

    private static IResult GetOrderById(int id)
    {
        Order? order = DataStore.Orders.Find(o => o.Id == id);
        return order is null ? Results.NotFound("Order not found!") : Results.Ok(order);
    }

    private static IResult CreateOrder(OrderDTO createOrderDTO)
    {
        var product = DataStore.Products.FirstOrDefault(p => p.Id == createOrderDTO.ProductId);
        if (product is null)
        {
            return Results.BadRequest("Product not found.");
        }

        var customer = DataStore.Customers.FirstOrDefault(c => c.Username == createOrderDTO.Username);
        if (customer is null)
        {
            return Results.BadRequest("Customer not found.");
        }

        Order order = new(
            DataStore.Orders.Count + 1,
            product,
            customer
        );
        DataStore.Orders.Add(order);
        return Results.CreatedAtRoute(GetOrderEndpoint, new { id = order.Id }, order);
    }

    private static IResult UpdateOrderById(int id, OrderDTO updateOrderDTO)
    {
        var index = DataStore.Orders.FindIndex(o => o.Id == id);
        if (index == -1) return Results.NotFound("Order with id not found.");

        var product = DataStore.Products.FirstOrDefault(p => p.Id == updateOrderDTO.ProductId);
        if (product is null)
        {
            return Results.BadRequest("Product not found.");
        }

        var customer = DataStore.Customers.FirstOrDefault(c => c.Username == updateOrderDTO.Username);
        if (customer is null)
        {
            return Results.BadRequest("Customer not found.");
        }

        DataStore.Orders[index] = new(
            id,
            product,
            customer
        );
        return Results.NoContent();
    }

    private static IResult DeleteOrderById(int id)
    {
        var order = DataStore.Orders.FirstOrDefault(o => o.Id == id);
        if (order is null) return Results.NotFound();

        DataStore.Orders.Remove(order);
        return Results.NoContent();
    }

    public static RouteGroupBuilder MapOrderEndpoint(this WebApplication app)
    {
        var group = app.MapGroup("orders")
                       .WithParameterValidation()
                       .WithOpenApi();

        group.MapGet("/", GetOrders);
        group.MapGet("/{id}", GetOrderById).WithName(GetOrderEndpoint);
        group.MapPost("/", CreateOrder);
        group.MapPut("/{id}", UpdateOrderById);
        group.MapDelete("/{id}", DeleteOrderById);

        return group;
    }
}
