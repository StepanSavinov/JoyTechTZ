using JoyTech.Tz.API.Models;
using JoyTech.Tz.BLL.Interfaces;
using JoyTech.Tz.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JoyTech.Tz.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderLogic _orderLogic;
    public OrderController(IOrderLogic orderLogic)
    {
        _orderLogic = orderLogic;
    }
    
    [Route("orders/all-orders")]
    [HttpGet]
    public IActionResult GetAllOrders()
    {
        return Ok(_orderLogic.GetAllOrders());
    }
    
    [Route("orders/create-order")]
    [HttpPost]
    public IActionResult CreateOrder(OrderModel model)
    {
        var order = new Order
        {
            Quantity = model.Quantity, TotalCost = model.TotalCost, UserId = model.UserId
        };
        
        if (_orderLogic.CreateOrder(order))
        {
            return Ok(order);
        }
        return BadRequest(order);
    }
    
    [Route("orders/update-order")]
    [HttpPost]
    public IActionResult UpdateOrder(int id, OrderModel model)
    {
        var order = _orderLogic.GetOrderById(id);

        if (order != null)
        {
            order.Quantity = model.Quantity;
            order.TotalCost = model.TotalCost;
            order.UserId = model.UserId;

            if (_orderLogic.UpdateOrder(order))
            {
                return Ok();
            }

            return BadRequest();
        }

        return NotFound();
    }
    
    [Route("orders/delete-order")]
    [HttpPost]
    public IActionResult DeleteOrder(int id)
    {
        if (_orderLogic.DeleteOrder(id))
        {
            return Ok();
        }
        
        return NotFound();
    }
}