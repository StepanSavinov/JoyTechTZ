using Microsoft.Extensions.Configuration;

namespace JoyTech.Tz.DAL;

public class SqlConfig
{
    public readonly string? ConnectionString;
    public SqlConfig(IConfiguration config)
    {
        ConnectionString = config.GetConnectionString("DefaultConnection");
    }
}