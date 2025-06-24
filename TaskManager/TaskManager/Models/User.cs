using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public enum UserRole { User, Admin}

    public class User
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        public UserRole Role { get; set; } = UserRole.User;

        public List<Task>? Tasks { get; set; } = new();
    }
}
