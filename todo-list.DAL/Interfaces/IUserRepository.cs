using todo_list.DAL.DTO;
using todo_list.DAL.Models;

namespace todo_list.DAL.Interfaces;

public interface IUserRepository
{
    void userRegistration(UserDTO userDto);
    List<UserModel> getAllUsers();
    bool userLogin(LoginDTO loginDto);
    UserModel getUserById(int id);
    bool userLogout(LoginDTO loginDto);
}
