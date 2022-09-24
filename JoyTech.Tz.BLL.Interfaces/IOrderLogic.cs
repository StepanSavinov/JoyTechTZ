using JoyTech.Tz.Entities;

namespace JoyTech.Tz.BLL.Interfaces;

public interface IOrderLogic
{
    bool CreateOrder(Order order);
    List<Order> GetAllOrders();
    bool UpdateOrder(Order order);
    bool DeleteOrder(int id);
    Order? GetOrderById(int id);
    List<Product> GetProductsInOrder(int orderId);
    
}