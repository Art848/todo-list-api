using todo_list.DAL.DTO;
using todo_list.DAL.Entities;
using todo_list.DAL.Models;

namespace todo_list.DAL.Interfaces;

public interface IUserRepository
{
    void userRegistration(User user);
    User getByUsername(string username);
    List<User> getNonAdminUsers();
}
