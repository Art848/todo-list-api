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
        var token = _userService.userLogin(loginDto);

        if (token == null)
            return Unauthorized(new { message = "Սխալ օգտանուն կամ գաղտնաբառ" });

        return Ok(new { token = token });
    }
}
