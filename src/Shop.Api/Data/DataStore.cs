using Shop.Api.Models;

namespace Shop.Api.Data;
public static class DataStore
{
    public static readonly List<Product> Products = [];
    public static readonly List<Customer> Customers = [];
    public static readonly List<Order> Orders = [];
}