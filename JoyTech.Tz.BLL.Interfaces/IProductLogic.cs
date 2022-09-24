using JoyTech.Tz.Entities;

namespace JoyTech.Tz.BLL.Interfaces;

public interface IProductLogic
{
    bool AddProduct(Product product);
    List<Product> GetAllProducts();
    bool UpdateProduct(Product product);
    bool DeleteProduct(int id);
    Product? GetProductById(int id);
    List<Product> GetProductsByIds(List<int> productsIds);
}