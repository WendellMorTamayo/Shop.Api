namespace Shop.Api.Models;

public class Order(int id, Product product, Customer customer)
{
    public int Id { get; set; } = id;
    public Product Name { get; set; } = product;
    public Customer Buyer { get; set; } = customer;
}