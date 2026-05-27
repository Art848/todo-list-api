using todo_list.DAL.Entities;
using todo_list.DAL.Interfaces;

namespace todo_list.DAL.Repositories;

public class TaskItemRepository : ITaskItemRepository
{
    private ApplicationDBContext _dbContext;

    public TaskItemRepository(ApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void createTask(TaskItem task)
    {
        _dbContext.TaskItems.Add(task);
        _dbContext.SaveChanges();
    }

    public List<TaskItem> getAllTasksOfAllUsers()
    {
        return _dbContext.TaskItems.ToList();
    }

    public List<TaskItem> getAllTasksOfUser(int userId)
    {
        return _dbContext.TaskItems.Where(t => t.UserId == userId).ToList();
    }

    public void updateTask(TaskItem task)
    {
        _dbContext.TaskItems.Update(task);
        _dbContext.SaveChanges();
    }

    public void deleteTask(TaskItem task)
    {
        _dbContext.TaskItems.Remove(task);
        _dbContext.SaveChanges();
    }

    public List<TaskItem> searchAllTasksContainingTitle(string search)
    {
        return _dbContext.TaskItems.Where(t => t.Title.Contains(search)).ToList();
    }

    public TaskItem getTaskById(int id)
    {
        return _dbContext.TaskItems.FirstOrDefault(t => t.Id == id);
    }

    public User getAdminUser()
    {
        return _dbContext.Users.FirstOrDefault(x => x.IsAdmin);
    }
}