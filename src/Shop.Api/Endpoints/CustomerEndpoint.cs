using Shop.Api.Models;
using Shop.Api.Models.DTO;

namespace Shop.Api.Endpoints;
static class CustomerEndpoint
{
    const string GetCustomerEndpoint = "GetCustomer";
    private static readonly List<Customer> Customers = [];

    public static List<Customer> GetCustomers()
    {
        return Customers;
    }

    public static IResult GetCustomerById(int id)
    {
        Customer? customer = Customers.Find(c => c.Id == id);

        return customer is null ? Results.NotFound("Customer not found!") : Results.Ok(customer);
    }

    public static IResult CreateCustomer(CustomerDTO createCustomerDTO)
    {
        var existingUsername = Customers.FirstOrDefault((c) => c.Username == createCustomerDTO.Username);
        if (existingUsername is not null)
        {
            return Results.BadRequest("Username already exists.");
        }
        Customer customer = new(
            Customers.Count + 1,
            createCustomerDTO.Username,
            createCustomerDTO.Firstname,
            createCustomerDTO.Lastname,
            createCustomerDTO.Email
        );
        Customers.Add(customer);
        return Results.CreatedAtRoute(GetCustomerEndpoint, new { id = customer.Id }, customer);
    }

    public static IResult UpdateCustomerByUsername(int id, CustomerDTO updateCustomerDTO)
    {
        var index = Customers.FindIndex((c) => c.Id == id);
        if (index == -1)
        {
            return Results.BadRequest("Customer with id not found.");
        }

        var existingUsername = Customers.FirstOrDefault((c) => c.Username == updateCustomerDTO.Username);
        if (existingUsername is not null)
        {
            return Results.BadRequest("Username already exists.");
        }

        Customers[index] = new(
            id,
            updateCustomerDTO.Username,
            updateCustomerDTO.Firstname,
            updateCustomerDTO.Lastname,
            updateCustomerDTO.Email
        );
        return Results.NoContent();
    }

    public static IResult DeleteCustomerByUsername(string username)
    {
        var customer = Customers.FirstOrDefault(p => p.Username == username);
        if (customer is null) return Results.NotFound();

        Customers.Remove(customer);
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