using JoyTech.Tz.Entities;

namespace JoyTech.Tz.API.Models;

public class OrderModel
{
    public int UserId { get; set; }
    public List<Product> Products { get; set; }
}