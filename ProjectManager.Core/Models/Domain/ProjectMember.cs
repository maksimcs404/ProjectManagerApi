using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Common.Enums;
using ProjectManager.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Core.Models.Domain
{
    public class ProjectMember : IEntity
    {
        public int Id { get; private set; }
        public MemberRole Role { get; private set; }
        public int UserId { get; private set; }
        public int ProjectId { get; private set; }
        public User User { get; private set; } = null!;

        private ProjectMember(int id, MemberRole role, int userId, int projectId)
        {
            Id = id;
            Role = role;
            UserId = userId;
            ProjectId = projectId;
        }

        public static Result<ProjectMember> Create(MemberRole role, int userId, int projectId)
        {
            var member = new ProjectMember(0, role, userId, projectId);
            return Result<ProjectMember>.Ok(member);
        }
    }
}
