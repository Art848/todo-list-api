using todo_list.DAL.DTO;
using todo_list.DAL.Models;

namespace todo_list.DAL.Interfaces;

public interface ITaskItemRepository
{
    void CreateTask(TaskItemDTO dto, int userId);
    List<TaskItemModel> GetAllTasksOfUser(int userId);
    List<TaskItemModel> GetAllTasksOfAllUsers();
    void UpdateTask(int id, TaskItemModel updatedTask);
    void DeleteTask(int id);
    List<TaskItemModel> SearchAllTasksContainingTitle(string search);
}
