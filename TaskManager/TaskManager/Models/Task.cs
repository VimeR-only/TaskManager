namespace TaskManager.Models
{
    public enum taskStatus { Done, inProgress, Waiting }

    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public taskStatus Status { get; set; } = taskStatus.Waiting;

        public int UserId { get; set; }
    }
}
