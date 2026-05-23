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

    public string userLogin(LoginDTO loginDto)
    {
        return _userRepository.userLogin(loginDto);
    }
}
