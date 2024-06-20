using Shop.Api.Models;
using Shop.Api.Models.DTO;

namespace Shop.Api.Endpoints;
public static class ProductEndpoint
{
    const string GET_PRODUCT_ENDPOINT = "GetProduct";
    private static readonly List<Product> Products = [];

    public static List<Product> GetProducts()
    {
        return Products;
    }

    public static IResult GetProductById(int id)
    {
        Product? product = Products.Find(p => p.Id == id);

        return product is null ? Results.NotFound() : Results.Ok(product);
    }

    public static IResult CreateProduct(ProductDTO createProductDTO)
    {
        Product product = new(
            Products.Count + 1,
            createProductDTO.Name,
            createProductDTO.Description,
            createProductDTO.Price
        );
        Products.Add(product);
        return Results.CreatedAtRoute(GET_PRODUCT_ENDPOINT, new { id = product.Id }, product);
    }

    public static IResult UpdateProductById(int id, ProductDTO updateProductDTO)
    {
        var index = Products.FindIndex(p => p.Id == id);
        if (index == -1) return Results.NotFound();

        Products[index] = new Product(
            id,
            updateProductDTO.Name,
            updateProductDTO.Description,
            updateProductDTO.Price
        );
        return Results.NoContent();
    }

    public static IResult DeleteProductById(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product is null) return Results.NotFound();

        Products.Remove(product);
        return Results.NoContent();
    }

    public static RouteGroupBuilder MapProductEndpoint(this WebApplication app)
    {
        var group = app.MapGroup("products")
                       .WithParameterValidation()
                       .WithOpenApi();

        group.MapGet("/", GetProducts);
        group.MapGet("/{id}", GetProductById).WithName(GET_PRODUCT_ENDPOINT);
        group.MapPost("/", CreateProduct);
        group.MapPut("/{id}", UpdateProductById);
        group.MapDelete("/{id}", DeleteProductById);

        return group;
    }
}