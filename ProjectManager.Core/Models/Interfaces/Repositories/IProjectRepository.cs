using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Common.Enums;
using ProjectManager.Core.Models.Domain;
using ProjectManager.Core.Models.Interfaces.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Core.Models.Interfaces.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        public Result<bool> DeleteProjectMemberById(int id);
        public Result<ProjectMember> GetProjectMember(int projectId, int userId);
        public List<Project> GetMyProjects(int userId);
        public List<Project> GetOtherProjects(int userId);
        public Result<ProjectMember> AddMemberToProject(int userId, int projectId, MemberRole role);
    }
}
