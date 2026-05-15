using ProjectManager.Core.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Application.Interfaces
{
    public interface IUserService : IService<Core.Models.Domain.User, DTO.Request.CreateUserRequest, DTO.Request.UpdateUserRequest>
    {
        User? GetByUsername(string username);
    }
}
