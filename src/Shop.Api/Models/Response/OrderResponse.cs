using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Models.Response;

public record class OrderResponse(
    [Required]
    [StringLength(50)]
    string Username,

    [Required]
    string ProductName,

    [Range(1, 100_000)]
    decimal Price
);
