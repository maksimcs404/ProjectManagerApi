using Microsoft.EntityFrameworkCore;
using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Domain;
using ProjectManager.Core.Models.Interfaces.Repositories;
using ProjectManager.Data.Context;
using ProjectManager.Data.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Data.Repositories

{
    //TODO: Доделать
    public class TaskRepository : BaseRepository<ProjectTask>, ITaskRepository
    {
        private readonly DbSet<TaskMember> _taskMembers;
        public TaskRepository(EfContext context) : base(context)
        {
            _taskMembers = _context.Set<TaskMember>();
        }
        public Result<List<ProjectTask>> GetAllOwnTasksByUserId(int userId)
        {
            try
            {
                var list = _dbSet.Where(p => p.OwnerId == userId).ToList();
                return Result<List<ProjectTask>>.Ok(list);
            }
            catch (Exception ex)
            {

                return Result<List<ProjectTask>>.Fail(ex.Message);
            }        
        }
        public Result<List<ProjectTask>> GetAllOtherTasksByUserId(int userId)
        {
            try
            {
                var tasks =  _taskMembers
                    .Where(u => u.UserId == userId)
                    .Select(u => u.Task)
                    .ToList();

                return Result<List<ProjectTask>>.Ok(tasks);
            } catch (Exception ex)
            {
                return Result<List<ProjectTask>>.Fail($"{ex.Message}");
            }
        }

    }
}
