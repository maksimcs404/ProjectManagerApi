using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Domain;
using ProjectManager.Core.Models.Interfaces.Repositories;
using ProjectManager.Core.Models.Interfaces.Repositories.Common;
using ProjectManager.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Data.Repositories.Common
{
    public class UserRepository : BaseRepository<Core.Models.Domain.User>, IUserRepository
    {
        public UserRepository(EfContext context) : base(context)
        {
        }
        public Result<User> GetByUserName(string userName)
        {
            try
            {
                var user = _dbSet.FirstOrDefault(u => u.UserName == userName);
                if (user != null)
                    return Result<User>.Ok(user);
                else
                    return Result<User>.Fail("User not found.");
            }
            catch (Exception ex)
            {
                return Result<User>.Fail($"An error occurred: {ex.Message}");
            }
        }
    }
}
