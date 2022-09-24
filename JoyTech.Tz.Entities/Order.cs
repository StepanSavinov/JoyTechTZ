using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace JoyTech.Tz.Entities;

[Table("Orders")]
[Index("Id", IsUnique=true, Name = "Id_Index")]
public sealed class Order : IEquatable<Order>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id")]
    public int Id { get; set; }
    
    [Column("UserId")]
    public int UserId { get; set; }
    
    [ForeignKey("UserId")]
    [JsonIgnore]
    public User User { get; set; }

    public List<Product> Products { get; set; }
    public List<OrderProducts> OrderProducts { get; set; } = new List<OrderProducts>();

    [Column("TotalCost")]
    public int TotalCost { get; set; }
    
    [Column("Quantity")]
    public int Quantity { get; set; }

    // public Order(List<Product> products, int userId)
    // {
    //     UserId = userId;
    //     Products = products;
    //     TotalCost = Products.Sum(p => p.Price);
    //     Quantity = Products.Count;
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