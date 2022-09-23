using JoyTech.Tz.Entities;

namespace JoyTech.Tz.BLL.Interfaces;

public interface IOrderLogic
{
    Task CreateOrder(User user);
    Task<List<Order>> GetAllOrders();
    Task UpdateOrder(Order order);
    Task DeleteOrder(Order order);
}