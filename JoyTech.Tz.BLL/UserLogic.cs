using JoyTech.Tz.BLL.Interfaces;
using JoyTech.Tz.DAL.Interfaces;
using JoyTech.Tz.Entities;

namespace JoyTech.Tz.BLL;

public class UserLogic : IUserLogic
{
    private readonly IUserDao _userDao;

    public UserLogic(IUserDao userDao)
    {
        _userDao = userDao;
    }

    public User? Auth(string username, string password)
    {
        return _userDao.Auth(username, password).Result;
    }

    public bool Register(User user)
    {
        return _userDao.Register(user).Result;
    }

    public List<User> GetAllUsers()
    {
        return _userDao.GetAllUsers().Result;
    }

    public bool UpdateUser(User user)
    {
        return _userDao.UpdateUser(user).Result;
    }

    public bool DeleteUser(int id)
    {
        var user = GetUserById(id);
        if (user is null)
        {
            return false;
        }

        return _userDao.DeleteUser(id).Result;
    }

    public User? GetUserById(int id)
    {
        return _userDao.GetUserById(id).Result;
    }

    public List<Order>? GetUserOrders(int id)
    {
        return GetUserById(id)?.Orders;
    }

    public string GetUserInfo(int id)
    {
        var user = GetUserById(id);
        if (user == null)
        {
            throw new NullReferenceException();
        }

        if (user.Orders != null)
        {
            return $"{user.Username}, orders: {user.Orders.Count}, sum: {user.Orders.Sum(o => o.TotalCost)}";
        }

        return $"This user hasn't order anything yet.";
    }
}