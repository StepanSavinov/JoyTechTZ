using JoyTech.Tz.Entities;
using Microsoft.EntityFrameworkCore;

namespace JoyTech.Tz.DAL;

public sealed class ApplicationContext : DbContext
{
    private readonly string? _connectionString;

    public DbSet<Order> Orders { get; set; }
    public DbSet<User> Users { get; set; }

    public ApplicationContext(SqlConfig config)
    {
        _connectionString = config.ConnectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
}