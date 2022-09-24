using JoyTech.Tz.BLL.Interfaces;
using JoyTech.Tz.DAL.Interfaces;
using JoyTech.Tz.Entities;

namespace JoyTech.Tz.BLL;

public class OrderLogic : IOrderLogic
{
    private readonly IOrderDao _orderDao;

    public OrderLogic(IOrderDao orderDao)
    {
        _orderDao = orderDao;
    }
    public bool CreateOrder(Order order)
    {
        return _orderDao.CreateOrder(order).Result;
    }

    public List<Order> GetAllOrders()
    {
        return _orderDao.GetAllOrders().Result;
    }

    public bool UpdateOrder(Order order)
    {
        return _orderDao.UpdateOrder(order).Result;
    }

    public bool DeleteOrder(int id)
    {
        var order = GetOrderById(id);
        if (order is null)
        {
            return false;
        }
        
        return _orderDao.DeleteOrder(id).Result;
    }

    public Order? GetOrderById(int id)
    {
        return _orderDao.GetOrderById(id).Result;
    }
}