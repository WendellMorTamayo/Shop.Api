using Shop.Api.Models.DTO;
using Shop.Api.Services;

namespace Shop.Api.Endpoints;

public class CustomerEndpoint : IEndpointModule
{
    public void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/customers")
                       .WithParameterValidation()
                       .WithOpenApi()
                       .WithTags("Customers");

        group.MapGet("/", (CustomerService customerService) => customerService.GetCustomers());
        group.MapGet("/{id}", (int id, CustomerService customerService) => customerService.GetCustomerById(id)).WithName(CustomerService.GetCustomerEndpoint);
        group.MapPost("/", (CustomerRequest createCustomerRequest, CustomerService customerService) => customerService.CreateCustomer(createCustomerRequest));
        group.MapPut("/{id}", (int id, CustomerRequest updateCustomerRequest, CustomerService customerService) => customerService.UpdateCustomer(id, updateCustomerRequest));
        group.MapDelete("/{id}", (int id, CustomerService customerService) => customerService.DeleteCustomerById(id));
    }
}
