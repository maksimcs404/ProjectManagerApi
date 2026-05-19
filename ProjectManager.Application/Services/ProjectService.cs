using ProjectManager.Application.DTO.Request;
using ProjectManager.Application.Interfaces;
using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Common.Enums;
using ProjectManager.Core.Models.Domain;
using ProjectManager.Core.Models.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public Result<Project> UpdateProject(UpdateProjectRequest request, int projectId)
        {
            var existingProjectResult = _projectRepository.Get(projectId);
            if (existingProjectResult == null || existingProjectResult == default)
            {
                return Result<Project>.Fail("Project not found.");
            }
            if (request.Title != null)
            {
                existingProjectResult.Title = request.Title;
            }
            if (request.Description != null)
            {
                existingProjectResult.Description = request.Description;
            }
            return _projectRepository.UpdateProject(existingProjectResult);


        }
        public Result<bool> DeleteProjectMemberById(int id)
        {
            var result = _projectRepository.DeleteProjectMemberById(id);
            if (!result.IsSuccess)
                return Result<bool>.Fail(result.Error!);
            return Result<bool>.Ok(true);
        }
        public Result<bool> DeleteProjectById(int projectId)
        {
            var result = _projectRepository.Delete(projectId);
            if (!result.IsSuccess)
                return Result<bool>.Fail(result.Error!);
            return Result<bool>.Ok(true);
        }
        public Result<Project> GetProjectById(int projectId)
        {
            var project = _projectRepository.Get(projectId);
            if (project == null)
                return Result<Project>.Fail("Project not found.");
            return Result<Project>.Ok(project);
        }
        public Result<bool> IsOwnerOfTheProject(int userId, int projectId)
        {
            var result = GetProjectById(projectId);
            if (!result.IsSuccess)
                return Result<bool>.Fail(result.Error!);

            var project = result.Data!;
            if (project.OwnerId != userId)
                return Result<bool>.Fail("User is not the owner of the project.");
            else return Result<bool>.Ok(true);

        }
        public Result<Project> CreateProject(int ownerId, string title, string? description)
        {
            var projectResult = Project.Create(ownerId, title, description);
            if (!projectResult.IsSuccess)
                return Result<Project>.Fail(projectResult.Error!);

            var project = projectResult.Data;
            var createdProject = _projectRepository.Create(project!);
            return createdProject;
        }
        public Result<ProjectMember> AddMemberToProject(int projectId, int userId, MemberRole role)
        {
            var result = _projectRepository.AddMemberToProject(userId, projectId, role);
            if (!result.IsSuccess)
            {
                return Result<ProjectMember>.Fail(result.Error!);
            }

            var existUser = _projectRepository.GetProjectMember(projectId, userId);
            if (existUser.IsSuccess)
            {
                return Result<ProjectMember>.Fail("User is already exist in the project.");
            }
            return Result<ProjectMember>.Ok(result.Data!);
        }

        public Result<List<Project>> GetMyProjects(int userId)
        {
            try
            {
                var result = _projectRepository.GetMyProjects(userId);
                return Result<List<Project>>.Ok(result);
            }
            catch (Exception ex)
            {
                return Result<List<Project>>.Fail(ex.Message);
            }
        }
    }
}
