using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Domain;
using ProjectManager.Core.Models.Interfaces.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Core.Models.Interfaces.Repositories
{
    public interface ITaskRepository : IRepository<ProjectTask>
    {
        Result<List<ProjectTask>> GetAllOwnTasksByUserId(int userId);
        Result<List<ProjectTask>> GetAllOtherTasksByUserId(int userId);
    }
}
