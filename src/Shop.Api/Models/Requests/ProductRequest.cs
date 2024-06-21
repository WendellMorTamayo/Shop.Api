using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Models.DTO;
public record class ProductRequest(
    [Required]
    [StringLength(100)]
    string Name,

    [Required]
    [StringLength(50)]
    string Description,

    [Range(1, 100_000)]
    decimal Price
);
