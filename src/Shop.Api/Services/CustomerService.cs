using Shop.Api.Data;
using Shop.Api.Models;
using Shop.Api.Models.DTO;
using Shop.Api.Services.Interfaces;

namespace Shop.Api.Services;
public class CustomerService(DataStore dataStore) : ICustomerService
{
    private readonly DataStore _dataStore = dataStore;
    private const string GetCustomerEndpoint = "GetCustomer";

    public List<Customer> GetCustomers()
    {
        return _dataStore.Customers;
    }

    public IResult GetCustomerById(int id)
    {
        Customer? customer = _dataStore.Customers.Find(c => c.Id == id);

        if (customer is null) return Results.NotFound("Customer not found!");

        CustomerResponse customerResponse = new(customer.Username, customer.FirstName, customer.LastName, customer.Email);
        return Results.Ok(customerResponse);
    }

    public IResult CreateCustomer(CustomerRequest createCustomerRequest)
    {
        var existingUsername = _dataStore.Customers.FirstOrDefault(c => c.Username == createCustomerRequest.Username);
        if (existingUsername != null) return Results.BadRequest("Username already exists.");

        Customer customer = new(
            _dataStore.Customers.Count + 1,
            createCustomerRequest.Username,
            createCustomerRequest.FirstName,
            createCustomerRequest.LastName,
            createCustomerRequest.Email
        );
        _dataStore.Customers.Add(customer);
        return Results.CreatedAtRoute(GetCustomerEndpoint, new { id = customer.Id }, customer);
    }

    public IResult UpdateCustomer(int id, CustomerRequest updateCustomerRequest)
    {
        var existingCustomer = _dataStore.Customers.FirstOrDefault(c => c.Id == id);
        if (existingCustomer == null) return Results.BadRequest("Customer not found.");

        var existingUsername = _dataStore.Customers.FirstOrDefault(c => c.Username == updateCustomerRequest.Username && c.Id != id);
        if (existingUsername != null) return Results.BadRequest("Username already exists.");

        existingCustomer.Username = updateCustomerRequest.Username;
        existingCustomer.FirstName = updateCustomerRequest.FirstName;
        existingCustomer.LastName = updateCustomerRequest.LastName;
        existingCustomer.Email = updateCustomerRequest.Email;

        return Results.NoContent();
    }

    public IResult DeleteCustomerByUsername(string username)
    {
        var customer = _dataStore.Customers.FirstOrDefault(c => c.Username == username);
        if (customer == null) return Results.NotFound("Customer not found!");

        _dataStore.Customers.Remove(customer);
        return Results.NoContent();
    }
}
