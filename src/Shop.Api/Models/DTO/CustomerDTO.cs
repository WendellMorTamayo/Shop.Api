
using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Models.DTO;
public record class CustomerDTO(
    [Required][StringLength(50)] string Username,
    [Required][StringLength(50)] string Firstname,
    [Required][StringLength(50)] string Lastname,
    [Required][StringLength(50)] string Email
);
