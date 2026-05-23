using todo_list.DAL.DTO;
using todo_list.DAL.Entities;
using todo_list.DAL.Interfaces;
using todo_list.DAL.Models;

namespace todo_list.DAL.Repositories;

public class TaskItemRepository : ITaskItemRepository
{
    private ApplicationDBContext _dbContext;

    public TaskItemRepository(ApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateTask(TaskItemDTO dto, int userId)
    {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate >= DateTime.Today ? dto.DueDate : throw new Exception("DateTime must be future"),
                IsDone = false,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };

            _dbContext.TaskItems.Add(task);
            _dbContext.SaveChanges();
    }

    public List<TaskItemModel> GetAllTasksOfAllUsers()
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.IsAdmin);

        if (user.IsAdmin)
        {
            var tasks = _dbContext.TaskItems.ToList();

            var taskModels = new List<TaskItemModel>();

            foreach (var task in tasks)
            {
                var taskModel = new TaskItemModel
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    DueDate = task.DueDate,
                    IsDone = task.IsDone,
                    UserId = task.UserId
                };

                taskModels.Add(taskModel);
            }

            return taskModels;
        }
        else
        {
            throw new Exception("User must be logged in with ADMIN role");
        }
    }

    public List<TaskItemModel> GetAllTasksOfUser(int userId)
    {
        var tasks = _dbContext.TaskItems.Where(t => t.UserId == userId).ToList();

        var taskModels = new List<TaskItemModel>();

        foreach (var task in tasks)
        {
            var taskModel = new TaskItemModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                IsDone = task.IsDone,
                UserId = task.UserId
            };

            taskModels.Add(taskModel);
        }

        return taskModels;
    }

    public void UpdateTask(int id, TaskItemModel updatedTask)
    {
            var task = _dbContext.TaskItems.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                throw new Exception("This task does not exist for this user");
            }

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.DueDate = updatedTask.DueDate >= DateTime.Today ? updatedTask.DueDate : throw new Exception("DateTime must be future");
            task.IsDone = updatedTask.IsDone;

            _dbContext.TaskItems.Update(task);
            _dbContext.SaveChanges();
    }

    public void DeleteTask(int id)
    {
            var task = _dbContext.TaskItems.FirstOrDefault(x => x.Id == id);

            if (task == null)
            {
                throw new Exception("This task does not exist for this user");
            }

            _dbContext.TaskItems.Remove(task);
            _dbContext.SaveChanges();
    }

    public List<TaskItemModel> SearchAllTasksContainingTitle(string search)
    {
        var tasks = _dbContext.TaskItems.Where(t => t.Title.Contains(search)).ToList();

        var taskModels = new List<TaskItemModel>();

            foreach (var task in tasks)
            {
                var taskModel = new TaskItemModel
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    DueDate = task.DueDate,
                    IsDone = task.IsDone,
                    UserId = task.UserId
                };

                taskModels.Add(taskModel);
            }
            return taskModels;
    }
}