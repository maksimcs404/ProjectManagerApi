using ProjectManager.Application.DTO.Request;
using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Application.Interfaces
{
    public interface IUserService
    {
        Result<User> Create(CreateUserRequest userRequest);
        Result<User> Update(UpdateUserRequest userRequest);
        Result<bool> Delete(int id);
        User? Get(int id);
        User? GetByUsername(string username);
    }
}
