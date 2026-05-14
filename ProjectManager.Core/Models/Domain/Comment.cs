using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ProjectManager.Core.Models.Domain
{
    public class Comment : IEntity
    {
        private const int MaxDataLength = 128;
        private const int MaxTitleLength = 64;
        private const int MinDataLength = 1;
        public int Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Data { get; private set; }
        public string? Title { get; private set; }
        public int TaskId { get; private set; }

        private Comment(int id, DateTime createdAt, string data, string title, int taskId)
        {
            Id = id;
            CreatedAt = createdAt;
            Data = data;
            Title = title;
            TaskId = taskId;
        }

        public static Result<Comment> Create(string data, string title, int taskId)
        {
            // Validate data
            if (string.IsNullOrEmpty(data) || data.Length < MinDataLength || data.Length > MaxDataLength)
            {
                return Result<Comment>.Fail($"Data must be between {MinDataLength} and {MaxDataLength} characters.");
            }

            // Validate title
            if (title.Length > MaxTitleLength)
            {
                return Result<Comment>.Fail($"Title must be at most {MaxTitleLength} characters.");
            }
            return Result<Comment>.Ok(new Comment(0, DateTime.UtcNow, data, title, taskId));
        }
    }
}
