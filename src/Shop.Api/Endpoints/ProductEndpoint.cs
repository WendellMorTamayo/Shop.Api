using Shop.Api.Models.DTO;
using Shop.Api.Services;

namespace Shop.Api.Endpoints;

public static class ProductEndpoint
{
    private const string GetProductEndpoint = "GetProduct";

    public static RouteGroupBuilder MapProductEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/products")
                       .WithParameterValidation()
                       .WithOpenApi();

        group.MapGet("/", (ProductService productService) => productService.GetProducts());
        group.MapGet("/{id}", (int id, ProductService productService) => productService.GetProductById(id)).WithName(GetProductEndpoint);
        group.MapPost("/", (ProductRequest productRequest, ProductService productService) => productService.CreateProduct(productRequest));
        group.MapPut("/{id}", (int id, ProductRequest productRequest, ProductService productService) => productService.UpdateProductById(id, productRequest));
        group.MapDelete("/{id}", (int id, ProductService productService) => productService.DeleteProductById(id));

        return group;
    }
}
