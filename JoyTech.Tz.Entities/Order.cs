namespace JoyTech.Tz.Entities;

public class Order : IEquatable<Order>
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TotalCost { get; set; }
    public int Quantity { get; set; }

    public Order(User user)
    {
        UserId = user.Id;
    }

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