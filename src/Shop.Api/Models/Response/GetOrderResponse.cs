using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Models.Response;

public record class GetOrderResponse(
    [Required]
    Guid Id,

    [Required]
    [StringLength(50)]
    string Username,

    [Required]
    string ProductName,

    [Range(1, 100_000)]
    decimal Price
);
