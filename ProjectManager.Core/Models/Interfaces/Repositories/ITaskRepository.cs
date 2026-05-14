using ProjectManager.Core.Models.Domain;
using ProjectManager.Core.Models.Interfaces.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Core.Models.Interfaces.Repositories
{
    public interface ITaskRepository : IRepository<ProjectTask>
    {
    }
}
