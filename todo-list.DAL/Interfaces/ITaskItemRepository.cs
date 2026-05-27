using todo_list.DAL.Entities;

namespace todo_list.DAL.Interfaces;

public interface ITaskItemRepository
{
    void createTask(TaskItem task);
    List<TaskItem> getAllTasksOfAllUsers();
    List<TaskItem> getAllTasksOfUser(int userId);
    void updateTask(TaskItem task);
    void deleteTask(TaskItem task);
    List<TaskItem> searchAllTasksContainingTitle(string search);
    TaskItem getTaskById(int id);
    User getAdminUser();
}
