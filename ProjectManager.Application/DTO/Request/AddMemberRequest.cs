using ProjectManager.Core.Models.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ProjectManager.Application.DTO.Request
{
    public record AddMemberRequest
    {
        public int userId { get; set; }

        public MemberRole Role { get; set; }
    }
}
