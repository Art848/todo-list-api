using todo_list.DAL.DTO;
using todo_list.DAL.Models;

namespace todo_list.Services.Interfaces;

public interface IUserService
{
    void userRegistration(UserDTO userDto);
    List<UserModel> getAllUsers();
    string userLogin(LoginDTO loginDto);
}
