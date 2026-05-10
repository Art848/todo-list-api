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

    public void CreateTask(TaskItemDTO dto)
    {
        _taskItemRepository.CreateTask(dto);
    }
    public List<TaskItemModel> GetAllTasksOfUser()
    {
        return _taskItemRepository.GetAllTasksOfUser();
    }

    public List<TaskItemModel> GetAllTasksOfAllUsers()
    {
        return _taskItemRepository.GetAllTasksOfAllUsers();
    }

    public TaskItemModel GetTaskById(int taskId)
    {
        return _taskItemRepository.GetTaskById(taskId);
    }
    public void UpdateTask(int id, TaskItemModel updatedTask)
    {
        _taskItemRepository.UpdateTask(id, updatedTask);
    }
    public void DeleteTask(int id)
    {
        _taskItemRepository.DeleteTask(id);
    }
}
