using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Models;

public record Product
{
    [Key]
    public Guid Id { get; set; } = new();
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
}
