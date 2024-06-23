using Microsoft.EntityFrameworkCore;
using Shop.Api.Models;

namespace Shop.Api.Data;

public class DataStoreContext(DbContextOptions<DataStoreContext> options) : DbContext(options)
{
    public required DbSet<Product> Products { get; set; }
    public required DbSet<Customer> Customers { get; set; }
    public required DbSet<Order> Orders { get; set; }
}