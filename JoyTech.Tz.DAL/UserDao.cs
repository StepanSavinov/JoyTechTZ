using JoyTech.Tz.DAL.Interfaces;
using JoyTech.Tz.Entities;
using Microsoft.EntityFrameworkCore;

namespace JoyTech.Tz.DAL;

public class UserDao : IUserDao
{
    private readonly SqlConfig _config;

    public UserDao(SqlConfig config)
    {
        _config = config;
    }
    public async Task<User?> Auth(string username, string password)
    {
        await using var context = new ApplicationContext(_config);
        return await context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
    }

    public async Task<bool> Register(User user)
    {
        await using var context = new ApplicationContext(_config);
        await context.Users.AddAsync(user);

        context.ChangeTracker.DetectChanges();
        if (context.Entry(user).State == EntityState.Added)
        {
            await context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<List<User>> GetAllUsers()
    {
        await using var context = new ApplicationContext(_config);
        var users = await context.Users.Include(o => o.Orders).ToListAsync();
        return users;
    }

    public async Task<bool> UpdateUser(User user)
    {
        await using var context = new ApplicationContext(_config);
        var userForEditing = await GetUserById(user.Id);

        if (userForEditing != null)
        {
            userForEditing.Username = user.Username;
            userForEditing.Password = user.Password;
            userForEditing.Role = user.Role;
            
            context.ChangeTracker.DetectChanges();
            
            if (context.Entry(userForEditing).State == EntityState.Modified)
            {
                await context.SaveChangesAsync();
                return true;
            }
            
            return false;
        }

        return false;
    }

    public async Task<bool> DeleteUser(int id)
    {
        await using var context = new ApplicationContext(_config);
        var userForDeletion = await GetUserById(id);

        if (userForDeletion != null)
        {
            context.Users.Remove(userForDeletion);

            context.ChangeTracker.DetectChanges();
            
            if (context.Entry(userForDeletion).State == EntityState.Deleted)
            {
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        return false;
    }

    public async Task<User?> GetUserById(int id)
    {
        await using var context = new ApplicationContext(_config);
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        return user;
    }
}