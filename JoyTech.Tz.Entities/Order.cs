using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JoyTech.Tz.Entities;

[Table("Orders")]
[Index("Id", IsUnique=true, Name = "Id_Index")]
public class Order : IEquatable<Order>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id")]
    public int Id { get; set; }
    
    [Column("UserId")]
    public int UserId { get; set; }
    
    [ForeignKey("UserId")]
    public User User { get; set; }
    
    [Column("TotalCost")]
    public int TotalCost { get; set; }
    
    [Column("Quantity")]
    public int Quantity { get; set; }

    // public Order(User user)
    // {
    //     UserId = user.Id;
    // }

    public bool Equals(Order? other)
    {
        if (other is null)
        {
            return false;
        }

        return Id == other.Id;
    }

    public override bool Equals(object? obj) => Equals(obj as Order);
    public override int GetHashCode() => (Id).GetHashCode();
}