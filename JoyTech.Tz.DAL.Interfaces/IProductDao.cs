using JoyTech.Tz.Entities;

namespace JoyTech.Tz.DAL.Interfaces;

public interface IProductDao
{
    Task<bool> AddProduct(Product product);
    Task<List<Product>> GetAllProducts();
    Task<bool> UpdateProduct(Product product);
    Task<bool> DeleteProduct(int id);
    Task<Product?> GetProductById(int id);
}