using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Common.Enums;
using ProjectManager.Core.Models.Domain;
using ProjectManager.Core.Models.Interfaces.Repositories;
using ProjectManager.Data.Context;
using ProjectManager.Data.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;

namespace ProjectManager.Data.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        private readonly DbSet<ProjectMember> _projectMembers;
        public ProjectRepository(EfContext context) : base(context)
        {
            _projectMembers = context.Set<ProjectMember>();
        }
        public Result<ProjectMember> GetProjectMember(int projectId, int userId)
        {
            var projectMember = _projectMembers.FirstOrDefault(pm => pm.ProjectId == projectId && pm.UserId == userId);
            if (projectMember == null)
                return Result<ProjectMember>.Fail("Project member not found.");
            return Result<ProjectMember>.Ok(projectMember);
        }
        public List<Project> GetMyProjects(int userId)
        {
            return _dbSet.Where(p => p.OwnerId == userId).ToList() ?? new List<Project>();
        }
        public List<Project> GetOtherProjects(int userId)
        {
            return _dbSet.Where(p => p.OwnerId != userId).ToList() ?? new List<Project>();
        }
        public Result<ProjectMember> AddMemberToProject(int userId, int projectId, MemberRole role)
        {
            try
            {
                var resultCreatedProjectMember = ProjectMember.Create(role, userId, projectId);
                if (!resultCreatedProjectMember.IsSuccess)
                {
                    return Result<ProjectMember>.Fail(resultCreatedProjectMember.Error ?? "Failed to create project member.");
                }
                _projectMembers.Add(resultCreatedProjectMember.Data!);
                _context.SaveChanges();
                return Result<ProjectMember>.Ok(resultCreatedProjectMember.Data!);
            } catch (Exception ex)
            {
                return Result<ProjectMember>.Fail($"An error occurred while adding member to project: {ex.Message}");
            }
            
        }
    
    }
}
