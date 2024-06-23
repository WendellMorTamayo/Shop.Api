using Microsoft.EntityFrameworkCore;
using Shop.Api.Data;
using Shop.Api.Models;
using Shop.Api.Models.Requests;
using Shop.Api.Models.Response;

namespace Shop.Api.Services;

public class CustomerService(DataStoreContext dataStoreContext) : IShopService<CustomerRequest>
{
    private readonly DataStoreContext _dataStoreContext = dataStoreContext;
    public static readonly string GetCustomerEndpoint = "GetCustomer";

    public async Task<IResult> GetAll()
    {
        var customers = await _dataStoreContext.Customers
            .Select(c => new GetCustomerResponse(
                c.Id,
                c.Username,
                c.FirstName,
                c.LastName,
                c.Email
            ))
            .ToListAsync();
        return Results.Ok(customers);
    }

    public async Task<IResult> GetById(Guid id)
    {
        Customer? customer = await _dataStoreContext.Customers.FirstOrDefaultAsync(c => c.Id == id);

        if (customer is null) return Results.NotFound("Customer not found!");

        GetCustomerResponse getCustomerResponse = new(customer.Id, customer.Username, customer.FirstName, customer.LastName, customer.Email);
        return Results.Ok(getCustomerResponse);
    }

    public async Task<IResult> Create(CustomerRequest request)
    {
        try
        {
            var existingUsername = await _dataStoreContext.Customers.FirstOrDefaultAsync(c => c.Username == request.Username);
            if (existingUsername != null) return Results.BadRequest("Username already exists.");

            Customer customer = new()
            {
                Username = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };
            _dataStoreContext.Customers.Add(customer);
            await _dataStoreContext.SaveChangesAsync();
            return Results.CreatedAtRoute(GetCustomerEndpoint, new { id = customer.Id }, customer);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to create customer: {ex.Message}");
        }
    }

    public async Task<IResult> Update(Guid id, CustomerRequest request)
    {
        try
        {
            var existingCustomer = await _dataStoreContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (existingCustomer == null) return Results.BadRequest("Customer not found.");

            var existingUsername = await _dataStoreContext.Customers.FirstOrDefaultAsync(c => c.Username == request.Username && c.Id != id);
            if (existingUsername != null) return Results.BadRequest("Username already exists.");

            existingCustomer.Username = request.Username;
            existingCustomer.FirstName = request.FirstName;
            existingCustomer.LastName = request.LastName;
            existingCustomer.Email = request.Email;

            await _dataStoreContext.SaveChangesAsync();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to update customer: {ex.Message}");
        }

    }

    public async Task<IResult> Delete(Guid id)
    {
        try
        {
            var customer = await _dataStoreContext.Customers.FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                return Results.NotFound("Customer not found!");
            _dataStoreContext.Customers.Remove(customer);

            await _dataStoreContext.SaveChangesAsync();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to delete customer: {ex.Message}");
        }
    }
}
