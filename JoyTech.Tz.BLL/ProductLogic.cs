using JoyTech.Tz.BLL.Interfaces;
using JoyTech.Tz.DAL.Interfaces;
using JoyTech.Tz.Entities;

namespace JoyTech.Tz.BLL;

public class ProductLogic : IProductLogic
{
    private readonly IProductDao _productDao;

    public ProductLogic(IProductDao productDao)
    {
        _productDao = productDao;
    }

    public bool AddProduct(Product product)
    {
        return _productDao.AddProduct(product).Result;
    }

    public List<Product> GetAllProducts()
    {
        return _productDao.GetAllProducts().Result.OrderBy(p => p.Price).ToList();
    }

    public bool UpdateProduct(Product product)
    {
        return _productDao.UpdateProduct(product).Result;
    }

    public bool DeleteProduct(int id)
    {
        var product = GetProductById(id);
        if (product is null)
        {
            return false;
        }
        return _productDao.DeleteProduct(id).Result;
    }

    public Product? GetProductById(int id)
    {
        return _productDao.GetProductById(id).Result;
    }
    
    public List<Product> GetProductsByIds(List<int> productsIds)
    {
        var products = new List<Product>();

        foreach (var productId in productsIds)
        {
            var product = GetProductById(productId);
            if (product != null)
            {
                products.Add(product);
            }
        }

        return products;
    }
}