using JoyTech.Tz.Entities;

namespace JoyTech.Tz.DAL.Interfaces;

public interface IUserDao
{
    Task<User?> Auth(string username, string password);
    Task<bool> Register(User user);
    Task<List<User>> GetAllUsers();
    Task<bool> UpdateUser(User user);
    Task<bool> DeleteUser(int id);
    Task<User?> GetUserById(int id);
}