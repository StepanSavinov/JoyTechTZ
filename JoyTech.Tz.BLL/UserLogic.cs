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

    public User Auth(string username, string password)
    {
        throw new NotImplementedException();
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
}