using Shop.Api.Data;
using Shop.Api.Models;
using Shop.Api.Models.DTO;

namespace Shop.Api.Services;

public class OrderService(DataStore dataStore) : IShopService<OrderRequest>
{
    private const string GetOrderEndpoint = "GetOrder";
    private readonly DataStore _dataStore = dataStore;

    public IResult GetAll()
    {
        return Results.Ok(_dataStore.Orders);
    }

    public IResult GetById(int id)
    {
        Order? order = _dataStore.Orders.Where(o => o.Id == id).FirstOrDefault();
        if (order is null) return Results.NotFound("Order not found!");

        OrderResponse orderResponse = new(order.Buyer.Username, order.Product.Name, order.Product.Price);

        return Results.Ok(orderResponse);
    }

    public IResult Create(OrderRequest request)
    {
        Product? product = _dataStore.Products.FirstOrDefault(p => p.Id == request.ProductId);
        if (product is null) return Results.BadRequest("Product not found.");

        Customer? customer = _dataStore.Customers.FirstOrDefault(c => c.Username == request.Username);
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

    public IResult Update(int id, OrderRequest request)
    {
        var index = _dataStore.Orders.FindIndex(o => o.Id == id);
        if (index == -1) return Results.NotFound("Order with id not found.");

        Product? product = _dataStore.Products.FirstOrDefault(p => p.Id == request.ProductId);
        if (product is null) return Results.BadRequest("Product not found.");

        Customer? customer = _dataStore.Customers.FirstOrDefault(c => c.Username == request.Username);
        if (customer is null) return Results.BadRequest("Customer not found.");

        _dataStore.Orders[index] = new(
            id,
            product,
            customer
        );
        return Results.NoContent();
    }

    public IResult Delete(int id)
    {
        Order? order = _dataStore.Orders.FirstOrDefault(o => o.Id == id);
        if (order is null) return Results.NotFound();

        _dataStore.Orders.Remove(order);
        return Results.NoContent();
    }
}