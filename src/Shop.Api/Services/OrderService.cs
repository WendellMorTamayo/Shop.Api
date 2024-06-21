using Shop.Api.Data;
using Shop.Api.Models;
using Microsoft.EntityFrameworkCore;
using Shop.Api.Models.Requests;
using Shop.Api.Models.Response;

namespace Shop.Api.Services;

public class OrderService(DataStore dataStore) : IShopService<OrderRequest>
{
    private readonly DataStore _dataStore = dataStore;
    private const string GetOrderEndpoint = "GetOrder";

    public async Task<IResult> GetAll()
    {
        var orders = await _dataStore.Orders
            .Include(o => o.Product)
            .Include(o => o.Buyer)
            .Select(o => new OrderResponse(
                o.Buyer.Username,
                o.Product.Name,
                o.Product.Price
            ))
            .ToListAsync();
        return Results.Ok(orders);
    }


    public IResult GetById(int id)
    {
        var order = _dataStore.Orders.Include(o => o.Product).Include(o => o.Buyer).FirstOrDefault(o => o.Id == id);
        if (order is null) return Results.NotFound("Order not found!");

        OrderResponse orderResponse = new(order.Buyer.Username, order.Product.Name, order.Product.Price);
        return Results.Ok(orderResponse);
    }

    public IResult Create(OrderRequest request)
    {
        var product = _dataStore.Products.Find(request.ProductId);
        if (product is null) return Results.BadRequest("Product not found.");

        var customer = _dataStore.Customers.FirstOrDefault(c => c.Username == request.Username);
        if (customer is null) return Results.BadRequest("Customer not found.");

        Order order = new(_dataStore.Orders.Count() + 1)
        {
            Product = product,
            Buyer = customer
        };

        _dataStore.Orders.Add(order);
        _dataStore.SaveChanges();

        OrderResponse orderResponse = new(customer.Username, product.Name, product.Price);
        return Results.CreatedAtRoute(GetOrderEndpoint, new { id = order.Id }, orderResponse);
    }

    public IResult Update(int id, OrderRequest request)
    {
        var existingOrder = _dataStore.Orders.Include(o => o.Product).Include(o => o.Buyer).FirstOrDefault(o => o.Id == id);
        if (existingOrder == null) return Results.NotFound("Order not found.");

        var product = _dataStore.Products.Find(request.ProductId);
        if (product is null) return Results.BadRequest("Product not found.");

        var customer = _dataStore.Customers.FirstOrDefault(c => c.Username == request.Username);
        if (customer is null) return Results.BadRequest("Customer not found.");

        Order updatedOrder = new(id)
        {
            Product = product,
            Buyer = customer
        };

        _dataStore.Entry(existingOrder).CurrentValues.SetValues(updatedOrder);
        _dataStore.SaveChanges();

        return Results.NoContent();
    }

    public IResult Delete(int id)
    {
        var order = _dataStore.Orders.Find(id);
        if (order == null) return Results.NotFound();

        _dataStore.Orders.Remove(order);
        _dataStore.SaveChanges();
        return Results.NoContent();
    }
}
