using JoyTech.Tz.Entities;

namespace JoyTech.Tz.DAL.Interfaces;

public interface IOrderDao
{
    Task<bool> CreateOrder(Order order);
    Task<List<Order>> GetAllOrders();
    Task<bool> UpdateOrder(Order order);
    Task<bool> DeleteOrder(int id);
    Task<Order?> GetOrderById(int id);
}