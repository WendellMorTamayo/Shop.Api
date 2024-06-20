using Shop.Api.Data;
using Shop.Api.Models;
using Shop.Api.Models.DTO;
using Shop.Api.Services.Interfaces;

namespace Shop.Api.Services;

public class ProductService(DataStore dataStore) : IProductService
{
    private const string GetProductEndpoint = "GetProduct";
    private readonly DataStore _dataStore = dataStore;

    public IResult GetProducts()
    {
        return Results.Ok(_dataStore.Products);
    }

    public IResult GetProductById(int id)
    {
        Product? product = _dataStore.Products.Find(p => p.Id == id);

        return product is null ? Results.NotFound() : Results.Ok(product);
    }

    public IResult CreateProduct(ProductRequest createProductRequest)
    {
        Product product = new(
            _dataStore.Products.Count + 1,
            createProductRequest.Name,
            createProductRequest.Description,
            createProductRequest.Price
        );
        _dataStore.Products.Add(product);
        return Results.CreatedAtRoute(GetProductEndpoint, new { id = product.Id }, product);
    }

    public IResult UpdateProductById(int id, ProductRequest updateProductRequest)
    {
        var index = _dataStore.Products.FindIndex(p => p.Id == id);
        if (index == -1) return Results.NotFound("Product not found");

        _dataStore.Products[index] = new(
            id,
            updateProductRequest.Name,
            updateProductRequest.Description,
            updateProductRequest.Price
        );
        return Results.NoContent();
    }

    public IResult DeleteProductById(int id)
    {
        var product = _dataStore.Products.FirstOrDefault(p => p.Id == id);
        if (product is null) return Results.NotFound();

        _dataStore.Products.Remove(product);
        return Results.NoContent();
    }
}
