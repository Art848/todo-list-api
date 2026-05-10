using todo_list.DAL.DTO;
using todo_list.DAL.Entities;
using todo_list.DAL.Interfaces;
using todo_list.DAL.Models;

namespace todo_list.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private ApplicationDBContext _dbContext;

    public UserRepository(ApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void userRegistration(UserDTO userDto)
    {
        var users = _dbContext.Users.ToList();

        foreach (var item in users)
        {
            if (item.Username == userDto.Username)
            {
                throw new Exception("User with this username already exist");
            }
        }

        var user = new User
        {
            Username = userDto.Username,
            Password = userDto.Password,
            Email = userDto.Email,
            isLogged = false,
            IsAdmin = false,
            RegisteredDate = DateTime.Now,
        };

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }

    public List<UserModel> getAllUsers()
    {
        var admin = _dbContext.Users.FirstOrDefault(u => u.IsAdmin);

        if (admin.isLogged && admin.IsAdmin)
        {
            var users = _dbContext.Users.Where(u => u.IsAdmin == false).ToList();

            var usermodels = new List<UserModel>();

            foreach (var user in users)
            {
                var usermodel = new UserModel
                {
                    Id  = user.Id,
                    Username = user.Username,
                    Email = user.Email
                };

                usermodels.Add(usermodel);
            }

            return usermodels;
        }
        else
        {
            throw new Exception("User must be logged in with ADMIN role");
        }
    }

    public bool userLogin(LoginDTO loginDto)
    {
        var loggedUser = _dbContext.Users.FirstOrDefault(u => u.isLogged);
        if (loggedUser == null)
        {
            var users = _dbContext.Users.ToList();

            foreach (var user in users)
            {

                if (user.Username == loginDto.Username && user.Password == loginDto.Password)
                {
                    var userToChange = _dbContext.Users.FirstOrDefault(x => x.Username == loginDto.Username);
                    Console.WriteLine(userToChange.ToString());

                    userToChange.isLogged = true;
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    continue;
                }
            }

            return false;
        }
        else
        {
            throw new Exception("We have already logged user");
        }
    }

    public bool userLogout(LoginDTO loginDto)
    {
        var users = _dbContext.Users.ToList();

        foreach (var user in users)
        {
            if (user.Username == loginDto.Username)
            {
                var userToChange = _dbContext.Users.FirstOrDefault(x => x.Username == loginDto.Username);

                if (userToChange.isLogged)
                {
                    userToChange.isLogged = false;
                    _dbContext.SaveChanges();
                    return false;
                }
            }
            else
            {
                continue;
            }
        }

        return false;
    }


    public UserModel getUserById(int id)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.isLogged);

        if (user.isLogged && user.IsAdmin)
        {
            var userToGet = _dbContext.Users.FirstOrDefault(x => x.Id == id);
            if (userToGet.Id == 1)
            {
                throw new Exception("Something wrong!!! Admin want to get himself");
            }

            var usermodel = new UserModel
            {
                Username = userToGet?.Username,
                Email = userToGet?.Email
            };

            return usermodel;
        }
        else
        {
            throw new Exception("User must be logged in with ADMIN role");
        }

    }
}
