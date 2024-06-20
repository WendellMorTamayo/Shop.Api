using Shop.Api.Models;
using Shop.Api.Models.DTO;
using Shop.Api.Data;

namespace Shop.Api.Endpoints;
public static class CustomerEndpoint
{
    const string GetCustomerEndpoint = "GetCustomer";

    private static List<Customer> GetCustomers()
    {
        return DataStore.Customers;
    }
    
    private static IResult GetCustomerById(int id)
    {
        Customer? customer = DataStore.Customers.Find(c => c.Id == id);

        return customer is null ? Results.NotFound("Customer not found!") : Results.Ok(customer);
    }

    private static IResult CreateCustomer(CustomerDTO createCustomerDTO)
    {
        var existingUsername = DataStore.Customers.FirstOrDefault((c) => c.Username == createCustomerDTO.Username);
        if (existingUsername is not null)
        {
            return Results.BadRequest("Username already exists.");
        }
        Customer customer = new(
            DataStore.Customers.Count + 1,
            createCustomerDTO.Username,
            createCustomerDTO.FirstName,
            createCustomerDTO.LastName,
            createCustomerDTO.Email
        );
        DataStore.Customers.Add(customer);
        return Results.CreatedAtRoute(GetCustomerEndpoint, new { id = customer.Id }, customer);
    }

    private static IResult UpdateCustomerByUsername(int id, CustomerDTO updateCustomerDTO)
    {
        var index = DataStore.Customers.FindIndex((c) => c.Id == id);
        if (index == -1)
        {
            return Results.BadRequest("Customer with id not found.");
        }

        var existingUsername = DataStore.Customers.FirstOrDefault((c) => c.Username == updateCustomerDTO.Username);
        if (existingUsername is not null)
        {
            return Results.BadRequest("Username already exists.");
        }

        DataStore.Customers[index] = new(
            id,
            updateCustomerDTO.Username,
            updateCustomerDTO.FirstName,
            updateCustomerDTO.LastName,
            updateCustomerDTO.Email
        );

        return Results.NoContent();
    }

    private static IResult DeleteCustomerByUsername(string username)
    {
        var customer = DataStore.Customers.FirstOrDefault(p => p.Username == username);
        if (customer is null) return Results.NotFound();

        DataStore.Customers.Remove(customer);
        return Results.NoContent();
    }

    public static RouteGroupBuilder MapCustomerEndpoint(this WebApplication app)
    {
        var group = app.MapGroup("customers")
                       .WithParameterValidation()
                       .WithOpenApi();

        group.MapGet("/", GetCustomers);
        group.MapGet("/{id}", GetCustomerById).WithName(GetCustomerEndpoint);
        group.MapPost("/", CreateCustomer);
        group.MapPut("/{id}", UpdateCustomerByUsername);
        group.MapDelete("/{username}", DeleteCustomerByUsername);

        return group;
    }
}