using TaskManager.Models;
namespace TaskManager.DTOs
{
    public class TaskCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public taskStatus Status { get; set; } = taskStatus.Waiting;
    }
}
