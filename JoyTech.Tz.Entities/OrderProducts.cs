namespace JoyTech.Tz.Entities;

public class OrderProducts
{
    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }
}