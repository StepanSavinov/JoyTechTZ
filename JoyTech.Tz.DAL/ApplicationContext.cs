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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            //entity.ToTable("Orders");
            entity.HasOne(o => o.User)
                .WithMany(u => u.Orders);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasMany(u => u.Orders)
                .WithOne(o => o.User);
        });

    }
}