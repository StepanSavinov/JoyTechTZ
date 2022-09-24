using JoyTech.Tz.DAL.Interfaces;
using JoyTech.Tz.Entities;
using Microsoft.EntityFrameworkCore;

namespace JoyTech.Tz.DAL;

public class OrderDao : IOrderDao
{
    private readonly SqlConfig _config;
    public OrderDao(SqlConfig config)
    {
        _config = config;
    }
    public async Task<bool> CreateOrder(Order order)
    {
        await using var context = new ApplicationContext(_config);
        await context.Orders.AddAsync(order);

        context.ChangeTracker.DetectChanges();
        if (context.Entry(order).State == EntityState.Added)
        {
            await context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<List<Order>> GetAllOrders()
    {
        await using var context = new ApplicationContext(_config);
        var orders = await context.Orders.ToListAsync();
        return orders;
    }

    public async Task<Order?> GetOrderById(int id)
    {
        await using var context = new ApplicationContext(_config);
        var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        return order;
    }

    public async Task<bool> UpdateOrder(Order order)
    {
        await using var context = new ApplicationContext(_config);
        var orderForEditing = context.Orders.FirstOrDefaultAsync(o => o.Id == order.Id).Result;

        if (orderForEditing != null)
        {
            orderForEditing.Quantity = order.Quantity;
            orderForEditing.TotalCost = order.TotalCost;
            orderForEditing.UserId = order.UserId;
            
            context.ChangeTracker.DetectChanges();
            
            if (context.Entry(orderForEditing).State == EntityState.Modified)
            {
                await context.SaveChangesAsync();
                return true;
            }
            
            return false;
        }

        return false;
    }

    public async Task<bool> DeleteOrder(int id)
    {
        await using var context = new ApplicationContext(_config);
        var orderForDeletion = context.Orders.FirstOrDefaultAsync(o => o.Id == id).Result;

        if (orderForDeletion != null)
        {
            context.Orders.Remove(orderForDeletion);

            context.ChangeTracker.DetectChanges();
            
            if (context.Entry(orderForDeletion).State == EntityState.Deleted)
            {
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        return false;
    }
}