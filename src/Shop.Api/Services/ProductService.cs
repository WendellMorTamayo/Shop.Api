using Shop.Api.Data;
using Shop.Api.Models;
using Shop.Api.Models.DTO;

namespace Shop.Api.Services;

public class ProductService(DataStore dataStore)
{
    private const string GetProductEndpoint = "GetProduct";
    private readonly DataStore _dataStore = dataStore;

    public List<Product> GetProducts()
    {
        return _dataStore.Products;
    }

    public IResult GetProductById(int id)
    {
        Product? product = _dataStore.Products.Find(p => p.Id == id);

        return product is null ? Results.NotFound() : Results.Ok(product);
    }

    public IResult CreateProduct(ProductRequest createProductDTO)
    {
        Product product = new(
            _dataStore.Products.Count + 1,
            createProductDTO.Name,
            createProductDTO.Description,
            createProductDTO.Price
        );
        _dataStore.Products.Add(product);
        return Results.CreatedAtRoute(GetProductEndpoint, new { id = product.Id }, product);
    }

    public IResult UpdateProductById(int id, ProductRequest updateProductDTO)
    {
        var index = _dataStore.Products.FindIndex(p => p.Id == id);
        if (index == -1) return Results.NotFound("Product not found");

        _dataStore.Products[index] = new(
            id,
            updateProductDTO.Name,
            updateProductDTO.Description,
            updateProductDTO.Price
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
