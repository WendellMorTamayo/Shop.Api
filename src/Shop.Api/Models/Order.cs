using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Models;

public record Order(int Id)
{
    [Key]
    public int Id { get; set; } = Id;
    public required Product Product { get; set; }
    public required Customer Buyer { get; set; }
}