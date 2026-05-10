using Microsoft.AspNetCore.Mvc;
using todo_list.DAL.DTO;
using todo_list.DAL.Models;
using todo_list.Services.Interfaces;

namespace todo_list_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskItemController : ControllerBase
{
    private ITaskItemService _taskItemService;

    public TaskItemController(ITaskItemService taskItemService)
    {
        _taskItemService = taskItemService;
    }

    [HttpPost("createTask")]
    public void CreateTask(TaskItemDTO dto)
    {
        _taskItemService.CreateTask(dto);
    }

    [HttpGet("getAllTasksOfUser")]
    public List<TaskItemModel> GetAllTasksOfUser()
    {
        return _taskItemService.GetAllTasksOfUser();
    }

    [HttpGet("getAllTasksOfAllUsers")]
    public List<TaskItemModel> GetAllTasksOfAllUsers()
    {
        return _taskItemService.GetAllTasksOfAllUsers();
    }

    [HttpGet("getTaskById")]
    public TaskItemModel GetTaskById(int taskId)
    {
        return _taskItemService.GetTaskById(taskId);
    }

    [HttpPut("updateTask")]
    public void UpdateTask(int id, [FromBody] TaskItemModel updatedTask)
    {
        _taskItemService.UpdateTask(id, updatedTask);
    }

    [HttpDelete("deleteTask")]
    public void DeleteTask(int id)
    {
        _taskItemService.DeleteTask(id);
    }
}
