using todo_list.DAL.DTO;
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

    public void CreateTask(TaskItemDTO dto, int userId)
    {
        _taskItemRepository.CreateTask(dto, userId);
    }
    public List<TaskItemModel> GetAllTasksOfUser(int userId)
    {
        return _taskItemRepository.GetAllTasksOfUser(userId);
    }

    public List<TaskItemModel> GetAllTasksOfAllUsers()
    {
        return _taskItemRepository.GetAllTasksOfAllUsers();
    }
    public void UpdateTask(int id, TaskItemModel updatedTask)
    {
        _taskItemRepository.UpdateTask(id, updatedTask);
    }
    public void DeleteTask(int id)
    {
        _taskItemRepository.DeleteTask(id);
    }
    public List<TaskItemModel> SearchAllTasksContainingTitle(string search)
    {
        return _taskItemRepository.SearchAllTasksContainingTitle(search);
    }
}
