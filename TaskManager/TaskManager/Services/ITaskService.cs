using TaskManager.DTOs;
using TaskManager.Models;

namespace TaskManager.Services
{
    public interface ITaskService
    {
        Task<Models.Task> CreateTaskAsync(TaskCreateDto dto, int userId);
        Task<List<Models.Task>> GetAllTaskUserAsync(int userId);
        Task<Models.Task> GetTaskUserIdAsync(int userId, int taskId);
        Task<bool> DeleteTaskIdAsync(int userId, int taskId);
    }
}
