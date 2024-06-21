using Shop.Api.Models.DTO;
using Shop.Api.Services;

namespace Shop.Api.Endpoints;

public class ProductEndpoint : IEndpointModule
{
    public void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products")
                       .WithParameterValidation()
                       .WithOpenApi()
                       .WithTags("Products");

        group.MapGet("/", (ProductService productService) => productService.GetProducts());
        group.MapGet("/{id}", (int id, ProductService productService) => productService.GetProductById(id)).WithName(ProductService.GetProductEndpoint);
        group.MapPost("/", (ProductRequest productRequest, ProductService productService) => productService.CreateProduct(productRequest));
        group.MapPut("/{id}", (int id, ProductRequest productRequest, ProductService productService) => productService.UpdateProductById(id, productRequest));
        group.MapDelete("/{id}", (int id, ProductService productService) => productService.DeleteProductById(id));
    }
}
