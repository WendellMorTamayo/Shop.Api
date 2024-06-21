using Shop.Api.Data;
using Shop.Api.Models;
using Shop.Api.Models.DTO;

namespace Shop.Api.Services;

public class ProductService(DataStore dataStore) : IShopService<ProductRequest>
{
    private readonly DataStore _dataStore = dataStore;
    public static readonly string GetProductEndpoint = "GetProduct";

    public IResult GetAll()
    {
        return Results.Ok(_dataStore.Products);
    }

    public IResult GetById(int id)
    {
        Product? product = _dataStore.Products.Find(p => p.Id == id);

        return product is null ? Results.NotFound() : Results.Ok(product);
    }

    public IResult Create(ProductRequest request)
    {
        Product product = new(
            _dataStore.Products.Count + 1,
            request.Name,
            request.Description,
            request.Price
        );
        _dataStore.Products.Add(product);
        return Results.CreatedAtRoute(GetProductEndpoint, new { id = product.Id }, product);
    }

    public IResult Update(int id, ProductRequest request)
    {
        var index = _dataStore.Products.FindIndex(p => p.Id == id);
        if (index == -1) return Results.NotFound("Product not found");

        _dataStore.Products[index] = new(
            id,
            request.Name,
            request.Description,
            request.Price
        );
        return Results.NoContent();
    }

    public IResult Delete(int id)
    {
        var product = _dataStore.Products.FirstOrDefault(p => p.Id == id);
        if (product is null) return Results.NotFound();

        _dataStore.Products.Remove(product);
        return Results.NoContent();
    }
}
