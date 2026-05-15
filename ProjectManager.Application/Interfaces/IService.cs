using ProjectManager.Application.DTO.Request;
using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Application.Interfaces
{
    public interface IService<TEntity, TCreateUserRequest, TUpdateUserRequest> where TEntity : class, IEntity
    {
        TEntity? Get(int id);
        IEnumerable<TEntity> GetAll();
        Result<TEntity> Create(TCreateUserRequest entity);
        Result<TEntity> Update(TUpdateUserRequest entity);
        Result<bool> Delete(int id);
    }
}
