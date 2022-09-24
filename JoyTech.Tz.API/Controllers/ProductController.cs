using JoyTech.Tz.API.Models;
using JoyTech.Tz.BLL.Interfaces;
using JoyTech.Tz.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JoyTech.Tz.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductLogic _productLogic;

    public ProductController(IProductLogic productLogic)
    {
        _productLogic = productLogic;
    }

    [AllowAnonymous]
    [Route("/all-products")]
    [HttpGet]
    public IActionResult GetAllProducts()
    {
        return Ok(_productLogic.GetAllProducts());
    }
    
    [Authorize(Policy = "Admin")]
    [Route("/add-product")]
    [HttpPut]
    public IActionResult AddProduct(ProductModel model)
    {
        var product = new Product(model.Name, model.Price);

        if (_productLogic.AddProduct(product))
        {
            return Ok(product);
        }
        return BadRequest(product);
    }
    
    [Authorize(Policy = "Admin")]
    [Route("/update-product")]
    [HttpPatch]
    public IActionResult UpdateProduct(int id, ProductModel model)
    {
        var product = _productLogic.GetProductById(id);

        if (product != null)
        {
            product.Name = model.Name;
            product.Price = model.Price;

            if (_productLogic.UpdateProduct(product))
            {
                return Ok();
            }

            return BadRequest();
        }

        return NotFound();
    }
    
    [Authorize(Policy = "Admin")]
    [Route("/delete-product")]
    [HttpDelete]
    public IActionResult DeleteUser(int id)
    {
        if (_productLogic.DeleteProduct(id))
        {
            return Ok();
        }
        
        return NotFound();
    }
}