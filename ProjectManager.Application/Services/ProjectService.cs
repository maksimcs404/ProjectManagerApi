using ProjectManager.Application.Interfaces;
using ProjectManager.Core.Models.Common;
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

        public Result<Project> CreateProject(int ownerId, string title, string? description)
        {
            var projectResult = Project.Create(ownerId, title, description);
            if (!projectResult.IsSuccess)
                return Result<Project>.Fail(projectResult.Error!);

            var project = projectResult.Data;
            var createdProject = _projectRepository.Create(project!);
            return createdProject;
        }
    }
}
