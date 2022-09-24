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
    
    [Route("/all-orders")]
    [HttpGet]
    public IActionResult GetAllOrders()
    {
        return Ok(_orderLogic.GetAllOrders());
    }
    
    [Route("/create-order")]
    [HttpPut]
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
    
    [Route("/update-order")]
    [HttpPut]
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