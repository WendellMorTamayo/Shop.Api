using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Models.Response;
public record class ProductResponse
(
    [Required]
    [StringLength(100)]
    string Name,

    [Required]
    [StringLength(256)]
    string Description,

    [Range(1, 100_000)]
    decimal Price
);

