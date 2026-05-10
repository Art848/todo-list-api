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
    public bool userLogin(LoginDTO loginDto)
    {
        return _userService.userLogin(loginDto);
    }


    [HttpPost("userLogout")]
    public bool userLogout(LoginDTO loginDto)
    {
        return _userService.userLogout(loginDto);
    }

    [HttpGet("getUserById")]
    public UserModel getUserById(int id)
    {
        return _userService.getUserById(id);
    }
}
