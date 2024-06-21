using Shop.Api.Models;
using Shop.Api.Models.DTO;

namespace Shop.Api.Services.Interfaces;
public interface ICustomerService
{
    IResult GetCustomers();
    IResult GetCustomerById(int id);
    IResult CreateCustomer(CustomerRequest createCustomerRequest);
    IResult UpdateCustomer(int id, CustomerRequest updateCustomerRequest);
    IResult DeleteCustomerById(int id);
}
