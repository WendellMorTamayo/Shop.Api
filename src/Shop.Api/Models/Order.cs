using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Models;

public record Order(int id)
{
    [Key]
    public int Id { get; set; } = id;
    public required Product Product { get; set; }
    public required Customer Buyer { get; set; }
}