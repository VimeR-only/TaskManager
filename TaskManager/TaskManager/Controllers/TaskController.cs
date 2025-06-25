using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManager.DTOs;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<ActionResult<Models.Task>> CreateTaskAsync(TaskCreateDto dto)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var task = await _taskService.CreateTaskAsync(dto, userId);

            return Ok(task);
        }

        [HttpGet]
        public async Task<ActionResult<Models.Task>> GetAllTaskUserAsync()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var task = await _taskService.GetAllTaskUserAsync(userId);

            return Ok(task);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTaskUserIdAsync(int id)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var task = await _taskService.GetTaskUserIdAsync(userId, id);

            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteTaskIdAsync(int id)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            bool status = await _taskService.DeleteTaskIdAsync(userId, id);

            if (status)
            {
                return Ok($"Post {id} deleted.");
            }
            else
            {
                return BadRequest($"Failed to delete post {id}.");
            }
        }
    }
}
