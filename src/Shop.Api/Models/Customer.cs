using System.ComponentModel.DataAnnotations;

namespace Shop.Api.Models;

public record Customer(int Id)
{
    [Key]
    public int Id { get; set; } = Id;
    public required string Username { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
}