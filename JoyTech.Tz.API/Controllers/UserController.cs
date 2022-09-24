using JoyTech.Tz.API.Models;
using JoyTech.Tz.BLL.Interfaces;
using JoyTech.Tz.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JoyTech.Tz.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserLogic _userLogic;

    public UserController(IUserLogic userLogic)
    {
        _userLogic = userLogic;
    }
    
    [Route("/get-all-users")]
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        return Ok(_userLogic.GetAllUsers());
    }
    
    [Route("/register")]
    [HttpPut]
    public IActionResult Register(UserModel model)
    {
        var user = new User(model.Username, model.Password);

        if (_userLogic.Register(user))
        {
            return Ok(user);
        }
        return BadRequest(user);
    }
    
    [Route("/update-user")]
    [HttpPut]
    public IActionResult UpdateUser(int id, UserModel model)
    {
        var user = _userLogic.GetUserById(id);

        if (user != null)
        {
            user.Username = model.Username;
            user.Password = model.Password;
            user.Role = model.Role;

            if (_userLogic.UpdateUser(user))
            {
                return Ok();
            }

            return BadRequest();
        }

        return NotFound();
    }
    
    [Route("/delete-user")]
    [HttpDelete]
    public IActionResult DeleteUser(int id)
    {
        if (_userLogic.DeleteUser(id))
        {
            return Ok();
        }
        
        return NotFound();
    }
}