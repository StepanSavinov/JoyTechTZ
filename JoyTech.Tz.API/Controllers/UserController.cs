using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JoyTech.Tz.API.Models;
using JoyTech.Tz.BLL.Interfaces;
using JoyTech.Tz.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JoyTech.Tz.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserLogic _userLogic;
    private readonly IConfiguration _configuration;

    public UserController(IUserLogic userLogic, IConfiguration configuration)
    {
        _userLogic = userLogic;
        _configuration = configuration;
    }
    
    [AllowAnonymous]
    [Route("/login")]
    [HttpPost]
    public IActionResult Login(UserModel model)
    {
        if (!string.IsNullOrEmpty(model.Username) &&
            !string.IsNullOrEmpty(model.Password))
        {
            var user = _userLogic.Auth(model.Username.ToLower(), GetHashedPassword(model.Password));

            if (user is null)
            {
                return NotFound();
            }
                
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };
                
            var token = new JwtSecurityToken
            (
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                    SecurityAlgorithms.HmacSha256)
            );
                
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(tokenString);
        }
        
        return BadRequest("Invalid user credentials");
    }
    
    [Authorize(Policy = "Admin")]
    [Route("/all-users")]
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        return Ok(_userLogic.GetAllUsers());
    }
    
    [AllowAnonymous]
    [Route("/register")]
    [HttpPut]
    public IActionResult Register(UserModel model)
    {
        var user = new User(model.Username.ToLower(), GetHashedPassword(model.Password));

        if (_userLogic.Register(user))
        {
            return Ok(user);
        }
        return BadRequest(user);
    }
    
    [Authorize(Policy = "Admin")]
    [Route("/update-user")]
    [HttpPatch]
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
    
    [Authorize(Policy = "Admin")]
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
    
    [Authorize(Policy = "User")]
    [Route("/orders")]
    [HttpGet]
    public IActionResult GetProductsForUser(int id)
    {
        return Ok(_userLogic.GetUserOrders(id));
    }
    
    private static string GetHashedPassword(string password)
    {
        using var sha = SHA512.Create();
        var sb = new StringBuilder();
        foreach (var item in sha.ComputeHash(Encoding.Unicode.GetBytes(password)))
        {
            sb.Append(item.ToString("X2"));
        }

        return sb.ToString();
    }
}