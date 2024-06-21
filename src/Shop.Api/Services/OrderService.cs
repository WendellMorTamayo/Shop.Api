using Shop.Api.Data;
using Shop.Api.Models;
using Microsoft.EntityFrameworkCore;
using Shop.Api.Models.Requests;
using Shop.Api.Models.Response;

namespace Shop.Api.Services;

public class OrderService(DataStoreContext _dataStoreContext) : IShopService<OrderRequest>
{
    private readonly DataStoreContext _dataStoreContext = _dataStoreContext;
    private const string GetOrderEndpoint = "GetOrder";

    public async Task<IResult> GetAll()
    {
        var orders = await _dataStoreContext.Orders
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


    public async Task<IResult> GetById(int id)
    {
        try
        {
            var order = await _dataStoreContext.Orders
                .Include(o => o.Product)
                .Include(o => o.Buyer)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return Results.NotFound("Order not found!");

            GetOrderResponse getOrderResponse = new(order.Buyer.Username, order.Product.Name, order.Product.Price);
            return Results.Ok(getOrderResponse);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to fetch order: {ex.Message}");
        }
    }


    public async Task<IResult> Create(OrderRequest request)
    {
        var product = await _dataStoreContext.Products.Where(p => p.Id == request.ProductId).FirstOrDefaultAsync();
        if (product is null)
            return Results.BadRequest("Product not found.");

        var customer = await _dataStoreContext.Customers.FirstOrDefaultAsync(c => c.Username == request.Username);
        if (customer is null)
            return Results.BadRequest("Customer not found.");

        Order order = new(_dataStoreContext.Orders.Count() + 1)
        {
            Product = product,
            Buyer = customer
        };

        _dataStoreContext.Orders.Add(order);
        await _dataStoreContext.SaveChangesAsync();

        CreateOrderResponse createOrderResponse = new(customer.Username, product.Name, product.Price);
        return Results.CreatedAtRoute(GetOrderEndpoint, new { id = order.Id }, createOrderResponse);
    }

    public async Task<IResult> Update(int id, OrderRequest request)
    {
        try
        {
            var existingOrder = await _dataStoreContext.Orders
                .Include(o => o.Product)
                .Include(o => o.Buyer)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (existingOrder == null)
                return Results.NotFound("Order not found.");

            var product = await _dataStoreContext.Products
                .Where(p => p.Id == request.ProductId)
                .FirstOrDefaultAsync();

            if (product == null)
                return Results.BadRequest("Product not found.");

            var customer = await _dataStoreContext.Customers
                .FirstOrDefaultAsync(c => c.Username == request.Username);

            if (customer == null)
                return Results.BadRequest("Customer not found.");

            existingOrder.Product = product;
            existingOrder.Buyer = customer;

            await _dataStoreContext.SaveChangesAsync();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to update order: {ex.Message}");
        }
    }


    public async Task<IResult> Delete(int id)
    {
        try
        {
            var order = await _dataStoreContext.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (order == null) return Results.NotFound();

            _dataStoreContext.Orders.Remove(order);
            await _dataStoreContext.SaveChangesAsync();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to delete order: {ex.Message}");
        }

    }
}
