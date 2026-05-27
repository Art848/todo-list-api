using todo_list.DAL.DTO;
using todo_list.DAL.Models;

namespace todo_list.Services.Interfaces;

public interface ITaskItemService
{
    void createTask(TaskItemDTO dto, int userId);
    List<TaskItemModel> getAllTasksOfUser(int userId);
    List<TaskItemModel> getAllTasksOfAllUsers();
    void updateTask(int id, TaskItemModel updatedTask);
    void deleteTask(int id);
    List<TaskItemModel> searchAllTasksContainingTitle(string search);

}
