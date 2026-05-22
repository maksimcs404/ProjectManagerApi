using ProjectManager.Core.Models.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Application.DTO.Request
{
    public class CreateTaskRequest
    {
        public DateTime? DeadLine { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Core.Models.Common.Enums.TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
    }
}
