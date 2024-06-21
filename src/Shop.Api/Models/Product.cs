using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Models;

public record Product(int Id)
{
    [Key]
    public int Id { get; set; } = Id;
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
}
