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

    public List<Product> GetProductsInOrder(int orderId)
    {
        var order = GetOrderById(orderId);
        if (order is null)
        {
            throw new NullReferenceException();
        }
        
        return order.Products;
    }

    public dynamic GetOrdersByFilter(
        bool byMaxPrice = false, 
        bool byMinPrice = false, 
        bool fromDate = false, 
        bool byAvailability = false)
    {
        if (byMaxPrice)
        {
            return GetAllOrders().OrderByDescending(o => o.TotalCost).First();
        }
        if (byMinPrice)
        {
            return GetAllOrders().OrderBy(o => o.TotalCost).First();
        }

        if (byAvailability)
        {
            var productName = string.Empty;
            foreach (var order in GetAllOrders())
            {
                if (order.Products.FirstOrDefault(p => p.Name == productName) != null)
                {
                    return true;
                }
                return false;
            }
        }

        return GetAllOrders();
    }
}