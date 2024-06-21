using Shop.Api.Data;
using Shop.Api.Models;
using Shop.Api.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Shop.Api.Services;

public class CustomerService : IShopService<CustomerRequest>
{
    private readonly DataStore _dataStore;
    public static readonly string GetCustomerEndpoint = "GetCustomer";

    public CustomerService(DataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public IResult GetAll()
    {
        return Results.Ok(_dataStore.Customers.ToList());
    }

    public IResult GetById(int id)
    {
        Customer? customer = _dataStore.Customers.Find(id);

        if (customer is null) return Results.NotFound("Customer not found!");

        CustomerResponse customerResponse = new(customer.Username, customer.FirstName, customer.LastName, customer.Email);
        return Results.Ok(customerResponse);
    }

    public IResult Create(CustomerRequest request)
    {
        var existingUsername = _dataStore.Customers.FirstOrDefault(c => c.Username == request.Username);
        if (existingUsername != null) return Results.BadRequest("Username already exists.");

        Customer customer = new(_dataStore.Customers.Count() + 1)
        {
            Username = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };
        _dataStore.Customers.Add(customer);
        _dataStore.SaveChanges();
        return Results.CreatedAtRoute(GetCustomerEndpoint, new { id = customer.Id }, customer);
    }

    public IResult Update(int id, CustomerRequest request)
    {
        var existingCustomer = _dataStore.Customers.Find(id);
        if (existingCustomer == null) return Results.BadRequest("Customer not found.");

        var existingUsername = _dataStore.Customers.FirstOrDefault(c => c.Username == request.Username && c.Id != id);
        if (existingUsername != null) return Results.BadRequest("Username already exists.");

        existingCustomer.Username = request.Username;
        existingCustomer.FirstName = request.FirstName;
        existingCustomer.LastName = request.LastName;
        existingCustomer.Email = request.Email;

        _dataStore.SaveChanges();
        return Results.NoContent();
    }

    public IResult Delete(int id)
    {
        var customer = _dataStore.Customers.Find(id);
        if (customer == null) return Results.NotFound("Customer not found!");

        _dataStore.Customers.Remove(customer);
        _dataStore.SaveChanges();
        return Results.NoContent();
    }
}
