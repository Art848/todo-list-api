using todo_list.DAL.DTO;
using todo_list.DAL.Interfaces;
using todo_list.DAL.Models;
using todo_list.Services.Interfaces;

namespace todo_list.Services.Services;

public class UserService : IUserService
{
    private IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public List<UserModel> getAllUsers()
    {
        return _userRepository.getAllUsers();
    }

    public void userRegistration(UserDTO userDto)
    {
        _userRepository.userRegistration(userDto);
    }

    public bool userLogin(LoginDTO loginDto)
    {
        return _userRepository.userLogin(loginDto);
    }

    public bool userLogout(LoginDTO loginDto)
    {
        return _userRepository.userLogout(loginDto);
    }

    public UserModel getUserById(int id)
    {
        return _userRepository.getUserById(id);
    }
}
