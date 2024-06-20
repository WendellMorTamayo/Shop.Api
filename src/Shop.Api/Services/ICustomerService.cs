using Shop.Api.Models;
using Shop.Api.Models.DTO;

namespace Shop.Api.Services;
public interface ICustomerService
{
    List<Customer> GetCustomers();
    IResult GetCustomerById(int id);
    IResult CreateCustomer(CustomerRequest createCustomerRequest);
    IResult UpdateCustomer(int id, CustomerRequest updateCustomerRequest);
    IResult DeleteCustomerByUsername(string username);
}
