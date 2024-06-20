using Shop.Api.Data;
using Shop.Api.Models;
using Shop.Api.Models.DTO;
using Shop.Api.Services.Interfaces;

namespace Shop.Api.Services;

public class OrderService(DataStore dataStore) : IOrderService
{
    private const string GetOrderEndpoint = "GetOrder";
    private readonly DataStore _dataStore = dataStore;

    public IResult GetOrders()
    {
        return Results.Ok(_dataStore.Orders);
    }

    public IResult GetOrderById(int id)
    {
        Order? order = _dataStore.Orders.Where(o => o.Id == id).FirstOrDefault();
        if (order is null) return Results.NotFound("Order not found!");

        OrderResponse orderResponse = new(order.Buyer.Username, order.Product.Name, order.Product.Price);

        return Results.Ok(orderResponse);
    }

    public IResult CreateOrder(OrderRequest createOrderRequest)
    {
        Product? product = _dataStore.Products.FirstOrDefault(p => p.Id == createOrderRequest.ProductId);
        if (product is null) return Results.BadRequest("Product not found.");

        Customer? customer = _dataStore.Customers.FirstOrDefault(c => c.Username == createOrderRequest.Username);
        if (customer is null) return Results.BadRequest("Customer not found.");

        Order order = new(
            _dataStore.Orders.Count + 1,
            product,
            customer
        );
        _dataStore.Orders.Add(order);

        OrderResponse orderResponse = new(customer.Username, product.Name, product.Price);
        return Results.CreatedAtRoute(GetOrderEndpoint, new { id = order.Id }, orderResponse);
    }

    public IResult UpdateOrderById(int id, OrderRequest updateOrderRequest)
    {
        var index = _dataStore.Orders.FindIndex(o => o.Id == id);
        if (index == -1) return Results.NotFound("Order with id not found.");

        Product? product = _dataStore.Products.FirstOrDefault(p => p.Id == updateOrderRequest.ProductId);
        if (product is null) return Results.BadRequest("Product not found.");

        Customer? customer = _dataStore.Customers.FirstOrDefault(c => c.Username == updateOrderRequest.Username);
        if (customer is null) return Results.BadRequest("Customer not found.");

        _dataStore.Orders[index] = new(
            id,
            product,
            customer
        );
        return Results.NoContent();
    }

    public IResult DeleteOrderById(int id)
    {
        Order? order = _dataStore.Orders.FirstOrDefault(o => o.Id == id);
        if (order is null) return Results.NotFound();

        _dataStore.Orders.Remove(order);
        return Results.NoContent();
    }
}