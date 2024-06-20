using Shop.Api.Data;
using Shop.Api.Models;
using Shop.Api.Models.DTO;

namespace Shop.Api.Services;

public class OrderService(DataStore dataStore)
{
    private const string GetOrderEndpoint = "GetOrder";
    private readonly DataStore _dataStore = dataStore;

    public List<Order> GetOrders()
    {
        return _dataStore.Orders;
    }

    public IResult GetOrderById(int id)
    {
        Order? order = _dataStore.Orders.Find(o => o.Id == id);
        
        return order is null ? Results.NotFound("Order not found!") : Results.Ok(order);
    }

    public IResult CreateOrder(OrderRequest createOrderRequest)
    {
        var product = _dataStore.Products.FirstOrDefault(p => p.Id == createOrderRequest.ProductId);
        if (product is null)
        {
            return Results.BadRequest("Product not found.");
        }

        var customer = _dataStore.Customers.FirstOrDefault(c => c.Username == createOrderRequest.Username);
        if (customer is null)
        {
            return Results.BadRequest("Customer not found.");
        }

        Order order = new(
            _dataStore.Orders.Count + 1,
            product,
            customer
        );
        _dataStore.Orders.Add(order);
        return Results.CreatedAtRoute(GetOrderEndpoint, new { id = order.Id }, order);
    }

    public IResult UpdateOrderById(int id, OrderRequest updateOrderRequest)
    {
        var index = _dataStore.Orders.FindIndex(o => o.Id == id);
        if (index == -1) return Results.NotFound("Order with id not found.");

        var product = _dataStore.Products.FirstOrDefault(p => p.Id == updateOrderRequest.ProductId);
        if (product is null)
        {
            return Results.BadRequest("Product not found.");
        }

        var customer = _dataStore.Customers.FirstOrDefault(c => c.Username == updateOrderRequest.Username);
        if (customer is null)
        {
            return Results.BadRequest("Customer not found.");
        }

        _dataStore.Orders[index] = new(
            id,
            product,
            customer
        );
        return Results.NoContent();
    }

    public IResult DeleteOrderById(int id)
    {
        var order = _dataStore.Orders.FirstOrDefault(o => o.Id == id);
        if (order is null) return Results.NotFound();

        _dataStore.Orders.Remove(order);
        return Results.NoContent();
    }
}