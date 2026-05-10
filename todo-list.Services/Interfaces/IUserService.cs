using todo_list.DAL.DTO;
using todo_list.DAL.Models;

namespace todo_list.Services.Interfaces;

public interface IUserService
{
    void userRegistration(UserDTO userDto);
    List<UserModel> getAllUsers();
    bool userLogin(LoginDTO loginDto);
    UserModel getUserById(int id);
    bool userLogout(LoginDTO loginDto);
}
