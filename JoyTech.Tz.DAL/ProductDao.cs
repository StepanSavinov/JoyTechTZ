using JoyTech.Tz.DAL.Interfaces;
using JoyTech.Tz.Entities;
using Microsoft.EntityFrameworkCore;

namespace JoyTech.Tz.DAL;

public class ProductDao : IProductDao
{
    private readonly SqlConfig _config;

    public ProductDao(SqlConfig config)
    {
        _config = config;
    }

    public async Task<bool> AddProduct(Product product)
    {
        await using var context = new ApplicationContext(_config);
        await context.Products.AddAsync(product);

        context.ChangeTracker.DetectChanges();
        if (context.Entry(product).State == EntityState.Added)
        {
            await context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<List<Product>> GetAllProducts()
    {
        await using var context = new ApplicationContext(_config);
        var products = await context.Products.Include(p => p.OrderProducts)
            .ToListAsync();
        return products;
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        await using var context = new ApplicationContext(_config);
        var productForEditing = await GetProductById(product.Id);

        if (productForEditing != null)
        {
            productForEditing.Name = product.Name;
            productForEditing.Price = product.Price;

            context.ChangeTracker.DetectChanges();
            
            if (context.Entry(productForEditing).State == EntityState.Modified)
            {
                await context.SaveChangesAsync();
                return true;
            }
            
            return false;
        }

        return false;
    }

    public async Task<bool> DeleteProduct(int id)
    {
        await using var context = new ApplicationContext(_config);
        var productForDeletion = await GetProductById(id);

        if (productForDeletion != null)
        {
            context.Products.Remove(productForDeletion);

            context.ChangeTracker.DetectChanges();
            
            if (context.Entry(productForDeletion).State == EntityState.Deleted)
            {
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        return false;
    }

    public async Task<Product?> GetProductById(int id)
    {
        await using var context = new ApplicationContext(_config);
        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
        return product;
    }
}