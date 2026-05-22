using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Common.Enums;
using ProjectManager.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Core.Models.Domain
{
    public class TaskMember : IEntity
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public MemberRole Role { get; private set; }
        public int TaskId { get; private set; }

        public ProjectTask Task { get; set; }
        public User User { get; private set; } = null!;

        private TaskMember(int id, int userId, MemberRole role, int taskId)
        {
            Id = id;
            UserId = userId;
            Role = role;
            TaskId = taskId;
        }

        public static Result<TaskMember> Create(int userId, MemberRole role, int taskId)
        {
            var member = new TaskMember(0, userId, role, taskId);
            return Result<TaskMember>.Ok(member);
        }
    }
}
