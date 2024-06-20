using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Models;
public class Product(int id, string name, string description, decimal price)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public decimal Price { get; set; } = price;
}
