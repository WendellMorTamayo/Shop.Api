using Shop.Api.Models.DTO;
using Shop.Api.Services;

namespace Shop.Api.Endpoints;

public static class CustomerEndpoint
{
    private const string GetCustomerEndpoint = "GetCustomer";

    public static RouteGroupBuilder MapCustomerEndpoint(this WebApplication app)
    {
        var group = app.MapGroup("/customers")
                       .WithParameterValidation()
                       .WithOpenApi()
                       .WithTags("Customers"); ;

        group.MapGet("/", (CustomerService customerService) => customerService.GetCustomers());
        group.MapGet("/{id}", (int id, CustomerService customerService) => customerService.GetCustomerById(id)).WithName(GetCustomerEndpoint);
        group.MapPost("/", (CustomerRequest createCustomerRequest, CustomerService customerService) => customerService.CreateCustomer(createCustomerRequest));
        group.MapPut("/{id}", (int id, CustomerRequest updateCustomerRequest, CustomerService customerService) => customerService.UpdateCustomer(id, updateCustomerRequest));
        group.MapDelete("/{username}", (string username, CustomerService customerService) => customerService.DeleteCustomerByUsername(username));

        return group;
    }
}
