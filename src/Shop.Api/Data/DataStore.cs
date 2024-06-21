using Microsoft.EntityFrameworkCore;
using Shop.Api.Models;

namespace Shop.Api.Data;

public class DataStore(DbContextOptions<DataStore> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new { Id = 1, Name = "Fighting", Description = "", Price = 100M },
            new { Id = 2, Name = "Roleplaying", Description = "", Price = 100M },
            new { Id = 3, Name = "Sports", Description = "", Price = 100M },
            new { Id = 4, Name = "Racing", Description = "", Price = 100M },
            new { Id = 5, Name = "Kids and Family", Description = "", Price = 100M }
        );
    }
}
