using JoyTech.Tz.Entities;

namespace JoyTech.Tz.BLL.Interfaces;

public interface IUserLogic
{
    User Auth(string username, string password);
    bool Register(User user);
    List<User> GetAllUsers();
    bool UpdateUser(User user);
    bool DeleteUser(int id);
    User? GetUserById(int id);
}