using ProjectManager.Application.DTO.Request;
using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Common.Enums;
using ProjectManager.Core.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Application.Interfaces
{
    public interface IProjectService
    {
        Result<Project> UpdateProject(UpdateProjectRequest request, int projectId);
        Result<bool> DeleteProjectMemberById(int id);
        Result<bool> DeleteProjectById(int projectId);
        Result<Project> CreateProject(int ownerId, string title, string? description);
        Result<List<Project>> GetMyProjects(int userId);
        Result<ProjectMember> AddMemberToProject(int projectId, int userId, MemberRole role);
        Result<Project> GetProjectById(int projectId);
        Result<bool> IsOwnerOfTheProject(int userId, int projectId);
    }
}
