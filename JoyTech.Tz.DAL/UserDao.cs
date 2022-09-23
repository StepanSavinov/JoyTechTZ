using JoyTech.Tz.DAL.Interfaces;

namespace JoyTech.Tz.DAL;

public class UserDao : IUserDao
{
    private readonly string? _connectionString;

    public UserDao(SqlConfig config)
    {
        _connectionString = config.ConnectionString;
    }
}