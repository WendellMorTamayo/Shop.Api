using Shop.Api.Models.DTO;
using Shop.Api.Services;

namespace Shop.Api.Endpoints;

public static class CustomerEndpoint
{
    public static RouteGroupBuilder MapCustomerEndpoint(this WebApplication app)
    {
        return app.MapEndpoints<CustomerService, CustomerRequest>("/customers", "Customer");
    }
}
