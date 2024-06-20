using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Models.DTO;
public record class OrderDTO(
    [Required]
    [StringLength(50)]
    string Username,

    [Required]
    int ProductId,

    [Range(1, 100_000)]
    decimal Price
);