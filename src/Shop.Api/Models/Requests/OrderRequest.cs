using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Models.Requests;
public record class OrderRequest(
    [Required]
    [StringLength(50)]
    string Username,

    [Required]
    Guid ProductId,

    [Range(1, 100_000)]
    decimal Price
);