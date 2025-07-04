using TaskManager.Data;
using TaskManager.Models;
using TaskManager.DTOs;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<User?> GetUserAsync(int userId)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return null;

            return user;
        }

        public async Task<User?> UpdateUserAsync(int userId, UserUpdateDTOs dto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return null;

            user.UserName = dto.UserName;
            if (dto.Role.HasValue)
                user.Role = dto.Role.Value;

            _db.Users.Update(user);

            await _db.SaveChangesAsync();

            return user;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _db.Users.ToListAsync();
        }
    }
}