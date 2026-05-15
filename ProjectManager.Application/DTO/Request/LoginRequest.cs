using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Application.DTO.Request
{
    public record LoginRequest
    {
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
