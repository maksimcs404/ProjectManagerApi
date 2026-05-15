using ProjectManager.Application.DTO.Request;
using ProjectManager.Application.Interfaces;
using ProjectManager.Application.Services;
using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Domain;
using ProjectManager.Core.Models.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User? Get(int id)
        {
           return _userRepository.Get(id);          
        }
        public User? GetByUsername(string userName)
        {
            return _userRepository.GetByUserName(userName).Data;
        }
        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public Result<User> Create(CreateUserRequest userRequest)
        {
            // Null check
            if (userRequest == null)
                return Result<User>.Fail("User data cannot be null.");


            var existUserResult = _userRepository.GetByUserName(userRequest.UserName);
            if (existUserResult.IsSuccess)
                return Result<User>.Fail("User with the same username already exists.");

            var user = User.Create(userRequest.Name, userRequest.UserName, userRequest.Password, DateTime.UtcNow);
            if (!user.IsSuccess)
                return Result<User>.Fail(user.Error!);
            return _userRepository.Create(user.Data!);
        }
        public Result<User> Update(UpdateUserRequest userRequest) //TODO: ДОДЕЛАТЬ ЗАГЛУШКУ
        {
            return Result<User>.Fail("User update is not implemented yet.");
        }
        public Result<bool> Delete(int id) //TODO: ПРОВЕРКА ПРАВ НА УДАЛЕНИЕ ПОЛЬЗОВАТЕЛЕЙ
        {
            return _userRepository.Delete(id);
        }

    }
}
