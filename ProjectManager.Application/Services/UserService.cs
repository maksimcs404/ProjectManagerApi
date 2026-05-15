using ProjectManager.Core.Models.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        
    }
}
