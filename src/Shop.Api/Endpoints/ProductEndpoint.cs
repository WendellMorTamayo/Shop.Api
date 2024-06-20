using Shop.Api.Data;
using Shop.Api.Models;
using Shop.Api.Models.DTO;

namespace Shop.Api.Endpoints;
public static class ProductEndpoint
{
    const string GetProductEndpoint = "GetProduct";

    private static List<Product> GetProducts()
    {
        return DataStore.Products;
    }

    private static IResult GetProductById(int id)
    {
        Product? product = DataStore.Products.Find(p => p.Id == id);

        return product is null ? Results.NotFound() : Results.Ok(product);
    }

    private static IResult CreateProduct(ProductDTO createProductDTO)
    {
        Product product = new(
            DataStore.Products.Count + 1,
            createProductDTO.Name,
            createProductDTO.Description,
            createProductDTO.Price
        );
        DataStore.Products.Add(product);
        return Results.CreatedAtRoute(GetProductEndpoint, new { id = product.Id }, product);
    }

    private static IResult UpdateProductById(int id, ProductDTO updateProductDTO)
    {
        var index = DataStore.Products.FindIndex(p => p.Id == id);
        if (index == -1) return Results.NotFound("Product not found");

        DataStore.Products[index] = new(
            id,
            updateProductDTO.Name,
            updateProductDTO.Description,
            updateProductDTO.Price
        );
        return Results.NoContent();
    }

    private static IResult DeleteProductById(int id)
    {
        var product = DataStore.Products.FirstOrDefault(p => p.Id == id);
        if (product is null) return Results.NotFound();

        DataStore.Products.Remove(product);
        return Results.NoContent();
    }

    public static RouteGroupBuilder MapProductEndpoint(this WebApplication app)
    {
        var group = app.MapGroup("products")
                       .WithParameterValidation()
                       .WithOpenApi();

        group.MapGet("/", GetProducts);
        group.MapGet("/{id}", GetProductById).WithName(GetProductEndpoint);
        group.MapPost("/", CreateProduct);
        group.MapPut("/{id}", UpdateProductById);
        group.MapDelete("/{id}", DeleteProductById);

        return group;
    }
}