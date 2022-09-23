using JoyTech.Tz.BLL.Interfaces;
using JoyTech.Tz.DAL.Interfaces;
using JoyTech.Tz.Entities;

namespace JoyTech.Tz.BLL;

public class OrderLogic : IOrderLogic
{
    private IOrderDao _orderDao;

    public OrderLogic(IOrderDao orderDao)
    {
        _orderDao = orderDao;
    }
    public async Task CreateOrder(User user)
    {
        await _orderDao.CreateOrder(user);
    }

    public async Task<List<Order>> GetAllOrders()
    {
        return await _orderDao.GetAllOrders();
    }

    public async Task UpdateOrder(Order order)
    {
        await _orderDao.UpdateOrder(order);
    }

    public async Task DeleteOrder(Order order)
    {
        await _orderDao.DeleteOrder(order);
    }
}