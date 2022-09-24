using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JoyTech.Tz.Entities;

[Table("Users")]
[Index("Id", IsUnique=true, Name = "Id_Index")]
public sealed class User : IEquatable<User>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id")]
    public int Id { get; set; }
    [Column("Username")]
    public string Username { get; set; }
    [Column("Password")]
    public string Password { get; set; }
    public List<Order>? Orders { get; set; }
    [Column("Role")]
    public string Role { get; set; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
        Orders = new List<Order>();
        Role = "User";
    }

    public User(string username, string password, string role)
    {
        Username = username;
        Password = password;
        Role = role;
    }

    public bool Equals(User? other)
    {
        if (other is null)
        {
            return false;
        }

        return Username == other.Username;
    }

    public override bool Equals(object? obj) => Equals(obj as User);
    public override int GetHashCode() => (Username).GetHashCode();
}