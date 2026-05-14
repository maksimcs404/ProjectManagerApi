using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Common.Enums;
using ProjectManager.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Core.Models.Domain
{
    public class ProjectTask : IEntity
    {
        private const int MinTitleLength = 3;
        private const int MaxTitleLength = 32;
        private const int MaxDescriptionLength = 256;
    
        public int Id { get; private set; }
        public DateTime? DeadLine { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public Common.Enums.TaskStatus Status { get; private set; }
        public TaskPriority Priority { get; private set; }
        public int ProjectId { get; private set; }
        public int OwnerId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        //public List<TaskMember> TaskMembers { get; }
        public List<Comment> Comments { get; } = null!;
        public Project Project { get; } = null!;

        private ProjectTask(int id, DateTime? deadLine, string title, string? description, int projectId, int ownerId, DateTime createdAt,
            Common.Enums.TaskStatus status, TaskPriority priority)
        {
            Id = id;
            DeadLine = deadLine;
            Title = title;
            Description = description;
            ProjectId = projectId;
            OwnerId = ownerId;
            CreatedAt = createdAt;
            Status = status;
            Priority = priority;
        }
        
        public static Result<ProjectTask> Create(DateTime? deadLine, string title, string? description, int projectId, int ownerId,
            Common.Enums.TaskStatus status, TaskPriority priority)
        {
            // Title validation
            if (string.IsNullOrWhiteSpace(title))
                return Result<ProjectTask>.Fail("Title cannot be empty.");
            if (title.Length < MinTitleLength || title.Length > MaxTitleLength)
                return Result<ProjectTask>.Fail("Title must be between 3 and 32 characters.");

            // Description validation
            if (description != null && description.Length > MaxDescriptionLength)
                return Result<ProjectTask>.Fail("Description cannot exceed 256 characters.");

            var task = new ProjectTask(0, deadLine, title, description, projectId, ownerId, DateTime.Now, status, priority);
            return Result<ProjectTask>.Ok(task);
        }
    }
}
