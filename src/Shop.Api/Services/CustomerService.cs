using Shop.Api.Data;
using Shop.Api.Models;
using Shop.Api.Models.DTO;

namespace Shop.Api.Services;

public class CustomerService(DataStore dataStore) : IShopService<CustomerRequest>
{
    private readonly DataStore _dataStore = dataStore;
    private const string GetCustomerEndpoint = "GetCustomer";

    public IResult GetAll()
    {
        return Results.Ok(_dataStore.Customers);
    }

    public IResult GetById(int id)
    {
        Customer? customer = _dataStore.Customers.Find(c => c.Id == id);

        if (customer is null) return Results.NotFound("Customer not found!");

        CustomerResponse customerResponse = new(customer.Username, customer.FirstName, customer.LastName, customer.Email);
        return Results.Ok(customerResponse);
    }

    public IResult Create(CustomerRequest request)
    {
        var existingUsername = _dataStore.Customers.FirstOrDefault(c => c.Username == request.Username);
        if (existingUsername != null) return Results.BadRequest("Username already exists.");

        Customer customer = new(
            _dataStore.Customers.Count + 1,
            request.Username,
            request.FirstName,
            request.LastName,
            request.Email
        );
        _dataStore.Customers.Add(customer);
        return Results.CreatedAtRoute(GetCustomerEndpoint, new { id = customer.Id }, customer);
    }

    public IResult Update(int id, CustomerRequest request)
    {
        var existingCustomer = _dataStore.Customers.FirstOrDefault(c => c.Id == id);
        if (existingCustomer == null) return Results.BadRequest("Customer not found.");

        var existingUsername = _dataStore.Customers.FirstOrDefault(c => c.Username == request.Username && c.Id != id);
        if (existingUsername != null) return Results.BadRequest("Username already exists.");

        existingCustomer.Username = request.Username;
        existingCustomer.FirstName = request.FirstName;
        existingCustomer.LastName = request.LastName;
        existingCustomer.Email = request.Email;

        return Results.NoContent();
    }

    public IResult Delete(int id)
    {
        var customer = _dataStore.Customers.FirstOrDefault(c => c.Id == id);
        if (customer == null) return Results.NotFound("Customer not found!");

        _dataStore.Customers.Remove(customer);
        return Results.NoContent();
    }


}
