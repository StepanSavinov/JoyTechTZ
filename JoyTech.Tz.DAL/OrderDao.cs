using JoyTech.Tz.DAL.Interfaces;

namespace JoyTech.Tz.DAL;

public class OrderDao : IOrderDao
{
    private readonly string? _connectionString;

    public OrderDao(SqlConfig config)
    {
        _connectionString = config.ConnectionString;
    }
}