using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Application.Interfaces
{
    public interface IService<T> where T : class, IEntity 
    {
        T? Get(int id);
        IEnumerable<T> GetAll();
        Result<T> Create(T entity);
        Result<T> Update(T entity);
        Result<bool> Delete(int id);
    }
}
