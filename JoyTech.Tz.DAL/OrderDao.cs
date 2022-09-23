using JoyTech.Tz.DAL.Interfaces;
using JoyTech.Tz.Entities;
using Microsoft.EntityFrameworkCore;

namespace JoyTech.Tz.DAL;

public class OrderDao : IOrderDao
{
    //private readonly string? _connectionString;
    private readonly SqlConfig _config;
    public OrderDao(SqlConfig config)
    {
        _config = config;
    }
    public async Task CreateOrder(User user)
    {
        await using var context = new ApplicationContext(_config);

        await context.Orders.AddAsync(new Order(user));
    }

    public async Task<List<Order>> GetAllOrders()
    {
        await using var context = new ApplicationContext(_config);
        var orders = await context.Orders.ToListAsync();
        return orders;
    }

    public async Task UpdateOrder(Order order)
    {
        await using var context = new ApplicationContext(_config);
        var orderForEditing = context.Orders.FirstOrDefaultAsync(o => o.Id == order.Id).Result;

        if (orderForEditing != null)
        {
            orderForEditing.Quantity = order.Quantity;
            orderForEditing.TotalCost = order.TotalCost;
            orderForEditing.UserId = order.UserId;

            await context.SaveChangesAsync();
        }

    }

    public async Task DeleteOrder(Order order)
    {
        await using var context = new ApplicationContext(_config);
        var orderForDeletion = context.Orders.FirstOrDefaultAsync(o => o.Id == order.Id).Result;

        if (orderForDeletion != null)
        {
            context.Orders.Remove(orderForDeletion);
            await context.SaveChangesAsync();
        }
    }
}