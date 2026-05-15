using ProjectManager.Core.Models.Domain;
using ProjectManager.Core.Models.Interfaces.Repositories;
using ProjectManager.Data.Context;
using ProjectManager.Data.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Data.Repositories
{
    public class UserRepository : BaseRepository<User>,IUserRepository
    {
        public UserRepository(EfContext context) : base(context)
        {
        }
        
    }
}
