using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Models.Requests;
public record class UpdateProductRequest(
    [Required]
    [StringLength(100)]
    string Name,

    [Required]
    [StringLength(50)]
    string Description,

    [Range(1, 100_000)]
    decimal Price
);
