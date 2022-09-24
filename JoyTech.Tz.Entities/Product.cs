using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JoyTech.Tz.Entities;

[Table("Products")]
[Index("Id", IsUnique=true, Name = "Id_Index")]
public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id")]
    public int Id { get; set; }
    [Column("Name")]
    public string Name { get; set; }
    [Column("Price")]
    public int Price { get; set; }

    public List<Order> Orders { get; set; }
    public List<OrderProducts> OrderProducts { get; set; } = new List<OrderProducts>();
    public Product(string name, int price)
    {
        Name = name;
        Price = price;
    }
}