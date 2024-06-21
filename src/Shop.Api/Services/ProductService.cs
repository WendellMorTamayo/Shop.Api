using Shop.Api.Data;
using Shop.Api.Models;
using Shop.Api.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Shop.Api.Services;

public class ProductService : IShopService<ProductRequest>
{
    private readonly DataStore _dataStore;
    private const string GetProductEndpoint = "GetProduct";

    public ProductService(DataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public IResult GetAll()
    {
        return Results.Ok(_dataStore.Products.ToList());
    }

    public IResult GetById(int id)
    {
        var product = _dataStore.Products.Find(id);

        return product is null ? Results.NotFound() : Results.Ok(product);
    }

    public IResult Create(ProductRequest request)
    {
        Product product = new(_dataStore.Products.Count() + 1)
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        };
        _dataStore.Products.Add(product);
        _dataStore.SaveChanges();
        return Results.CreatedAtRoute(GetProductEndpoint, new { id = product.Id }, product);
    }

    public IResult Update(int id, ProductRequest request)
    {
        var product = _dataStore.Products.Find(id);
        if (product == null) return Results.NotFound("Product not found");

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;

        _dataStore.SaveChanges();
        return Results.NoContent();
    }

    public IResult Delete(int id)
    {
        var product = _dataStore.Products.Find(id);
        if (product is null) return Results.NotFound();

        _dataStore.Products.Remove(product);
        _dataStore.SaveChanges();
        return Results.NoContent();
    }
}
