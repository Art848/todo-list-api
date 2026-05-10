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

    public void CreateTask(TaskItemDTO dto)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.isLogged);

        if (user.isLogged)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate >= DateTime.Today ? dto.DueDate : throw new Exception("DateTime must be future"),
                IsDone = false,
                CreatedAt = DateTime.UtcNow,
                UserId = user.Id
            };

            _dbContext.TaskItems.Add(task);
            _dbContext.SaveChanges();
        }
        else
        {
            throw new Exception("User must be logged in");
        }
    }

    public List<TaskItemModel> GetAllTasksOfAllUsers()
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.isLogged);

        if (user.isLogged && user.IsAdmin)
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

    public List<TaskItemModel> GetAllTasksOfUser()
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.isLogged);

        if (user.isLogged)
        {
            var tasks = _dbContext.TaskItems.ToList().Where(t => t.UserId == user.Id);

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
            throw new Exception("User must be logged in");
        }
    }

    public TaskItemModel GetTaskById(int taskId)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.isLogged);

        if (user.isLogged)
        {
            var task = _dbContext.TaskItems.FirstOrDefault(t => t.UserId == user.Id && t.Id == taskId);

            var taskModel = new TaskItemModel
            {
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                IsDone = task.IsDone,
            };

            return taskModel;
        }
        else
        {
            throw new Exception("User must be logged in");
        }
    }

    public void UpdateTask(int id, TaskItemModel updatedTask)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.isLogged);

        if (user.isLogged && user.IsAdmin)
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
        else if (user.isLogged && user.IsAdmin == false)
        {
            var task = _dbContext.TaskItems.FirstOrDefault(t => t.Id == id && t.UserId == user.Id);
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
        else
        {
            throw new Exception("User must be logged in");
        }
    }

    public void DeleteTask(int id)
    {
        var user = _dbContext.Users.FirstOrDefault(x => x.isLogged);

        if (user.isLogged && user.IsAdmin)
        {
            var task = _dbContext.TaskItems.FirstOrDefault(x => x.Id == id);
            Console.WriteLine(task.Id);

            if (task == null)
            {
                throw new Exception("This task does not exist for this user");
            }

            _dbContext.TaskItems.Remove(task);
            _dbContext.SaveChanges();
        }
        else if (user.isLogged && user.IsAdmin == false)
        {
            var task = _dbContext.TaskItems.FirstOrDefault(x => x.Id == id && x.UserId == user.Id);

            if (task == null)
            {
                throw new Exception("This task does not exist for this user");
            }

            _dbContext.TaskItems.Remove(task);
            _dbContext.SaveChanges();
        }
        else
        {
            throw new Exception("User must be logged in");
        }
    }
}