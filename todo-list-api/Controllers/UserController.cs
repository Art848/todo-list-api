using Microsoft.AspNetCore.Mvc;
using todo_list.DAL.DTO;
using todo_list.DAL.Models;
using todo_list.Services.Interfaces;

namespace todo_list_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("userRegistration")]
    public void userRegistration(UserDTO userDto)
    {
        _userService.userRegistration(userDto);
    }

    [HttpGet("getAllUsers")]
    public List<UserModel> getAllUsers()
    {
        return _userService.getAllUsers();
    }

    [HttpPost("userLogin")]
    public IActionResult Login([FromBody] LoginDTO loginDto)
    {
        try
        {
            string token = _userService.userLogin(loginDto);

            return Ok(new { token = token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
