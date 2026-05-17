using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Application.Interfaces
{
    public interface IProjectService
    {
        Result<Project> CreateProject(int ownerId, string title, string? description);
    }
}
