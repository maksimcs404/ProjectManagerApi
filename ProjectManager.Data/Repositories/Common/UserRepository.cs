using ProjectManager.Core.Models.Interfaces.Repositories.Common;
using ProjectManager.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Data.Repositories.Common
{
    public class UserRepository : BaseRepository<Core.Models.Domain.User>, IRepository<Core.Models.Domain.User>
    {
        public UserRepository(EfContext context) : base(context)
        {
        }
    }
}
