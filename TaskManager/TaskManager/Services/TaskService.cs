﻿using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.DTOs;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _db;

        public TaskService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Models.Task> CreateTaskAsync(TaskCreateDto dto, int userId)
        {
            var task = new Models.Task
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                UserId = userId,
            };

            _db.Tasks.Add(task);

            await _db.SaveChangesAsync();

            return task;
        }

        public async Task<List<Models.Task>> GetAllTaskUserAsync(int userId)
        {
            var task = await _db.Tasks.Where(t => t.UserId == userId).ToListAsync();

            return task;
        }

        public async Task<Models.Task> GetTaskUserIdAsync(int userId, int taskId)
        {
            var task = await _db.Tasks.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == taskId);

            return task;
        }

        public async Task<bool> DeleteTaskIdAsync(int userId, int taskId)
        {
            var task = await _db.Tasks.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == taskId);

            if (task == null)
                return false;

            _db.Tasks.Remove(task);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Models.Task> UpdateTaskStatusAsync(int userId, int taskId, taskStatus status)
        {
            var task = await _db.Tasks.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == taskId);

            if (task == null)
                return null;

            task.Status = status;

            _db.Tasks.Update(task);

            await _db.SaveChangesAsync();

            return task;
        }

        public async Task<Models.Task?> GetTaskUserId(int userId, int taskId)
        {
            var task = await _db.Tasks.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == taskId);

            return task;
        }

        public async Task<Models.Task?> UpdateTaskAsync(int userId, int taskId, TaskUpdateDto dto)
        {
            var task = await _db.Tasks.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == taskId);

            if (task == null) return null;

            task.Title = dto.Title;
            task.Description = dto.Description;

            _db.Update(task);

            await _db.SaveChangesAsync();

            return task;
        }
    }
}
