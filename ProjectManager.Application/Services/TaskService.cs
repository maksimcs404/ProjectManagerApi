using ProjectManager.Application.DTO.Request;
using ProjectManager.Application.Interfaces;
using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Domain;
using ProjectManager.Core.Models.Interfaces.Repositories;
using ProjectManager.Core.Models.Interfaces.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Application.Services
{
    public class TaskService
        : ITaskService
    {
        private readonly ITaskRepository _repository;
        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public ProjectTask? Get(int id)
        {
            return _repository.Get(id);
        }
        public Result<List<ProjectTask>> GetAll(int userId)
        {
            var ownerTasks = _repository.GetAllOwnTasksByUserId(userId);
            var otherTasks = _repository.GetAllOtherTasksByUserId(userId);

            if (!ownerTasks.IsSuccess)
                return Result<List<ProjectTask>>.Fail(ownerTasks.Error!);

            if (!otherTasks.IsSuccess)
                return Result<List<ProjectTask>>.Fail(otherTasks.Error!);

            var allTasks = ownerTasks.Data!
                .Concat(otherTasks.Data!)
                .ToList();

            return Result<List<ProjectTask>>.Ok(allTasks);
        }
        public Result<ProjectTask> Create(CreateTaskRequest request, int ownerId, int ProjectId)
        {
            var task = ProjectTask.Create(deadLine: request.DeadLine, title: request.Title,
                description: request.Description, status: request.Status, priority: request.Priority, 
                projectId: ProjectId, ownerId: ownerId);

            if (!task.IsSuccess)
            {
                return Result<ProjectTask>.Fail(task.Error!);
            }

            return _repository.Create(task.Data!);
        }
    }
}
