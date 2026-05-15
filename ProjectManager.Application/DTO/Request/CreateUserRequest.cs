using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Application.DTO.Request
{
    public record CreateUserRequest
    {
        public string Name { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
