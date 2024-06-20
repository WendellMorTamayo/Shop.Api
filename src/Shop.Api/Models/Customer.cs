namespace Shop.Api.Models;

public class Customer(int id, string username, string firstName, string lastName, string email)
{
    public int Id { get; set; } = id;
    public string Username { get; set; } = username;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string Email { get; set; } = email;
}