using todo_list.DAL.DTO;
using todo_list.DAL.Entities;
using todo_list.DAL.Interfaces;
using todo_list.DAL.Models;
using todo_list.Services.Interfaces;

namespace todo_list.Services.Services;

public class TaskItemService : ITaskItemService
{
    private ITaskItemRepository _taskItemRepository;

    public TaskItemService(ITaskItemRepository taskItemRepository)
    {
        _taskItemRepository = taskItemRepository;
    }

    public void createTask(TaskItemDTO dto, int userId)
    {
        if (dto.DueDate < DateTime.Today)
        {
            throw new Exception("DateTime must be future");
        }

        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            DueDate = dto.DueDate,
            IsDone = false,
            CreatedAt = DateTime.UtcNow,
            UserId = userId
        };

        _taskItemRepository.createTask(task);
    }

    public List<TaskItemModel> getAllTasksOfAllUsers()
    {
        var user = _taskItemRepository.getAdminUser();
        if (user == null || !user.IsAdmin)
        {
            throw new Exception("User must be logged in with ADMIN role");
        }

        var tasks = _taskItemRepository.getAllTasksOfAllUsers();
        
        return mapToModelList(tasks);
    }

    public List<TaskItemModel> getAllTasksOfUser(int userId)
    {
        var tasks = _taskItemRepository.getAllTasksOfUser(userId);
        return mapToModelList(tasks);
    }

    public void updateTask(int id, TaskItemModel updatedTask)
    {
        var task = _taskItemRepository.getTaskById(id);
        if (task == null)
        {
            throw new Exception("This task does not exist for this user");
        }

        if (updatedTask.DueDate < DateTime.Today)
        {
            throw new Exception("DateTime must be future");
        }

        task.Title = updatedTask.Title;
        task.Description = updatedTask.Description;
        task.DueDate = updatedTask.DueDate;
        task.IsDone = updatedTask.IsDone;

        _taskItemRepository.updateTask(task);
    }

    public void deleteTask(int id)
    {
        var task = _taskItemRepository.getTaskById(id);
        if (task == null)
        {
            throw new Exception("This task does not exist for this user");
        }

        _taskItemRepository.deleteTask(task);
    }

    public List<TaskItemModel> searchAllTasksContainingTitle(string search)
    {
        var tasks = _taskItemRepository.searchAllTasksContainingTitle(search);
        return mapToModelList(tasks);
    }

    private List<TaskItemModel> mapToModelList(List<TaskItem> tasks)
    {
        var taskModels = new List<TaskItemModel>();
        foreach (var task in tasks)
        {
            taskModels.Add(new TaskItemModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                IsDone = task.IsDone,
                UserId = task.UserId
            });
        }
        return taskModels;
    }
}
