using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using todo_list.DAL.DTO;
using todo_list.DAL.Models;
using todo_list.Services.Interfaces;

namespace todo_list_api.Controllers;

[Authorize]
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
    [Authorize]
    public IActionResult CreateTask(TaskItemDTO dto, int userId)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "Օգտատերը վավերացված չէ:" });
            }

            int currentUserId = int.Parse(userIdClaim.Value);

            // Կանչում ենք սերվիսը, որն էլ իր հերթին կկանչի քո ռեպոզիտորիան
            _taskItemService.createTask(dto, currentUserId);

            return Ok(new { message = "Գործը հաջողությամբ ստեղծվեց:" });
        }
        catch (Exception ex)
        {
            // Եթե ռեպոզիտորիայում ամսաթվի ստուգումը ձախողվի, ֆրոնտենդին կուղարկվի 400 սխալը
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("getAllTasksOfUser")]
    public List<TaskItemModel> GetAllTasksOfUser()
    {
        // 1. Կարդում ենք օգտատիրոջ ID-ն JWT թոքենի միջից
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
        {
            throw new Exception("Unauthorized access");
        }

        int currentUserId = int.Parse(userIdClaim.Value);

        // 2. Փոխանցում ենք այդ ID-ն սերվիսին, որպեսզի միայն իր գործերը բերի
        return _taskItemService.getAllTasksOfUser(currentUserId);
    }

    [HttpGet("getAllTasksOfAllUsers")]
    public List<TaskItemModel> GetAllTasksOfAllUsers()
    {
        return _taskItemService.getAllTasksOfAllUsers();
    }

    [HttpPut("updateTask")]
    [Authorize]
    public void UpdateTask(int id, [FromBody] TaskItemModel updatedTask)
    {
        _taskItemService.updateTask(id, updatedTask);
    }

    [HttpDelete("deleteTask")]
    public void DeleteTask(int id)
    {
        _taskItemService.deleteTask(id);
    }

    [HttpPost("searchTaskByContainingTitle")]
    public List<TaskItemModel> SearchAllTasksContainingTitle([FromBody] string search)
    {
        return _taskItemService.searchAllTasksContainingTitle(search);
    }
}
