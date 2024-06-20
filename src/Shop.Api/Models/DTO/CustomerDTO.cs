using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Models.DTO;
public record class CustomerDTO(
    [Required]
    [StringLength(50)]
    string Username,

    [Required]
    [StringLength(50)]
    string FirstName,

    [Required]
    [StringLength(50)]
    string LastName,

    [Required]
    [StringLength(50)]
    [EmailAddress]
    string Email
);
