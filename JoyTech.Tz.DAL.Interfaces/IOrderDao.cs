using JoyTech.Tz.Entities;

namespace JoyTech.Tz.DAL.Interfaces;

public interface IOrderDao
{
    Task CreateOrder(User user);
    Task<List<Order>> GetAllOrders();
    Task UpdateOrder(Order order);
    Task DeleteOrder(Order order);
}