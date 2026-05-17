using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Application.DTO.Request
{
    public class CreateProjectRequest
    {
        public string Title { get; set; }
        public string? Description { get; set; } = string.Empty;
    }
}
