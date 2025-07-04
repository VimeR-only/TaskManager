using TaskManager.Models;

namespace TaskManager.DTOs
{
    public class UserGetDTOs
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}