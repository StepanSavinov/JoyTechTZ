using JoyTech.Tz.BLL;
using JoyTech.Tz.BLL.Interfaces;
using JoyTech.Tz.DAL;
using JoyTech.Tz.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JoyTech.Tz.DependencyConfig;

public static class Config
{
    public static IServiceProvider RegisterServices(IServiceCollection services)
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        services.AddSingleton<IConfiguration>(builder);
        services.AddScoped<IUserDao, UserDao>();
        services.AddScoped<IOrderDao, OrderDao>();
        services.AddScoped<IUserLogic, UserLogic>();
        services.AddScoped<IOrderLogic, OrderLogic>();
        services.AddSingleton(cfg => new SqlConfig(builder));
        
        return services.BuildServiceProvider();
    }
}