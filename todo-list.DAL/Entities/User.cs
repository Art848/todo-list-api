namespace todo_list.DAL.Entities;

using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }

    [MaxLength(100)]
    public string Password { get; set; }
    public bool isLogged { get; set; }

    public bool IsAdmin { get; set; }
    public DateTime RegisteredDate { get; set; }
}