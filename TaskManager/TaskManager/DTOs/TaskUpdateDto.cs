﻿using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTOs
{
    public class TaskUpdateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
