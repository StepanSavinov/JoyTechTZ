using JoyTech.Tz.API.Models;
using JoyTech.Tz.BLL.Interfaces;
using JoyTech.Tz.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JoyTech.Tz.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderLogic _orderLogic;
    private readonly IProductLogic _productLogic;
    public OrderController(IOrderLogic orderLogic, IProductLogic productLogic)
    {
        _orderLogic = orderLogic;
        _productLogic = productLogic;
    }
    
    [Authorize(Policy = "Admin")]
    [Route("/all-orders")]
    [HttpGet]
    public IActionResult GetAllOrders()
    {
        return Ok(_orderLogic.GetAllOrders());
    }
    
    [Authorize]
    [Route("/create-order")]
    [HttpPut]
    public IActionResult CreateOrder(OrderModel model)
    {
        var order = new Order
        {
            //OrderProducts = _productLogic.GetProductsByIds(model.Products),
            UserId = model.UserId,
            //Quantity = model.Products.Count,
            //TotalCost = _productLogic.GetProductsByIds(model.Products).Sum(p => p.Price)
        };
        
        if (_orderLogic.CreateOrder(order))
        {
            
            return Ok(order);
        }
        return BadRequest(order);
    }
    
    [Authorize(Policy = "Admin")]
    [Route("/update-order")]
    [HttpPatch]
    public IActionResult UpdateOrder(int id, OrderModel model)
    {
        var order = _orderLogic.GetOrderById(id);

        if (order != null)
        {
            //order.Products = _productLogic.GetProductsByIds(model.Products);
            order.UserId = model.UserId;

            if (_orderLogic.UpdateOrder(order))
            {
                return Ok();
            }

            return BadRequest();
        }

        return NotFound();
    }
    
    [Authorize]
    [Route("/delete-order")]
    [HttpDelete]
    public IActionResult DeleteOrder(int id)
    {
        if (_orderLogic.DeleteOrder(id))
        {
            return Ok();
        }
        
        return NotFound();
    }
}