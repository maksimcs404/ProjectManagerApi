using ProjectManager.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Core.Models.Interfaces.Repositories.Common
{
    public interface IRepository<T> where T : IEntity
    {
        public T Get(int id);
        public Result<T> Update(T entity);
        public Result<T> Delete(int id);
        public Result<T> Create(T entity);
    }
}
