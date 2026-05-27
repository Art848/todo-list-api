using BCrypt.Net;
using todo_list.DAL.Entities;
using todo_list.DAL.Interfaces;

namespace todo_list.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDBContext _dbContext;

    public UserRepository(ApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void userRegistration(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }

    public User getByUsername(string username)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Username == username);
    }

    public List<User> getNonAdminUsers()
    {
        return _dbContext.Users.Where(u => u.IsAdmin == false).ToList();
    }
}
