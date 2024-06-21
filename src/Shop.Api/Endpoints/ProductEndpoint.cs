using Shop.Api.Models.DTO;
using Shop.Api.Services;

namespace Shop.Api.Endpoints;

public static class ProductEndpoint
{
    public static RouteGroupBuilder MapProductEndpoint(this WebApplication app)
    {
        return app.MapEndpoints<ProductService, ProductRequest>("/products", "Product");
    }
}
