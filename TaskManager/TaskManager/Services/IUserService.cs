using TaskManager.Models;
using TaskManager.DTOs;

namespace TaskManager.Services
{
    public interface IUserService
    {
        Task<User?> GetUserAsync(int userId);
        Task<User> UpdateUserAsync(int userId, UserUpdateDTOs dto);
        Task<List<User>> GetAllUsersAsync();
    }
}
