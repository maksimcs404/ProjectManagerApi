using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ProjectManager.Core.Models.Domain
{
    public class Project : IEntity  
    {
        private const int MinTitleLength = 3;
        private const int MaxTitleLength = 32;
        private const int MaxDescriptionLength = 256;


        public int Id { get; }
        public int OwnerId { get; }
        public string Title { get; }
        public string? Description { get; } = string.Empty;
        public List<ProjectTask> ProjectTasks { get; set; } = null!;
        public List<ProjectMember> ProjectMembers { get; set; } = null!;

        private Project(int id, int ownerId, string title, string? description)
        {
            Id = id;
            OwnerId = ownerId;
            Title = title;
            Description = description;
        }

        public static Result<Project> Create(int ownerId, string title, string? description)
        {
            // Title validation
            if (string.IsNullOrWhiteSpace(title))
                return Result<Project>.Fail("Title cannot be empty.");
            if (title.Length < MinTitleLength || title.Length > MaxTitleLength)
                return Result<Project>.Fail($"Title must be between {MinTitleLength} and {MaxTitleLength} characters.");

            // Description validation
            if (description != null && description.Length > MaxDescriptionLength)
                return Result<Project>.Fail($"Description cannot exceed {MaxDescriptionLength} characters.");

            var project = new Project(0, ownerId, title, description);
            return Result<Project>.Ok(project);
        }
    }
}
