namespace Shop.Api.Models;
class Customer(int id, string firstname, string lastname, string email)
{
    public int Id { get; set; } = id;
    public string Firstname { get; set; } = firstname;
    public string Lastname { get; set; } = lastname;
    public string Email { get; set; } = email;
}