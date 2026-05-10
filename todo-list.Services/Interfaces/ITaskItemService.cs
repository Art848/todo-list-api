using todo_list.DAL.DTO;
using todo_list.DAL.Models;

namespace todo_list.Services.Interfaces;

public interface ITaskItemService
{
    void CreateTask(TaskItemDTO dto);
    List<TaskItemModel> GetAllTasksOfUser();
    List<TaskItemModel> GetAllTasksOfAllUsers();
    TaskItemModel GetTaskById(int taskId);
    void UpdateTask(int id, TaskItemModel updatedTask);
    void DeleteTask(int id);
}
