using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Models;

public record Order
{
    [Key]
    public Guid Id { get; set; } = new();
    public required Product Product { get; set; }
    public required Customer Buyer { get; set; }
}