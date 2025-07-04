using TaskManager.Models;

namespace TaskManager.DTOs
{
    public class UserUpdateDTOs
    {
        public string UserName { get; set; } = string.Empty;
        public UserRole ?Role { get; set; }
    }
}