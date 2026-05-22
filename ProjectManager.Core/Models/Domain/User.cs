using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Core.Models.Domain
{
    public class User : IEntity
    {
        private const int MinPasswordLength = 6;
        private const int MaxPasswordLength = 24;
        private const int MinNameLength = 3;
        private const int MaxNameLength = 32;
  
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string UserName { get; private set; } = null!;
        public string Password { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public List<Project> Projects { get; set; } = new List<Project>();
        public List<ProjectMember> ProjectMembers { get; private set; } = null!;
        public List<TaskMember> TaskMembers { get; set; }
        public List<ProjectTask> ProjectTasks { get; set; }
        
        protected User() { }
        private User (int id, string name, string userName, string password, DateTime? createdAt)
        {
            Id = id;
            Name = name;
            Password = password;
            CreatedAt = createdAt;
            UserName = userName;
        }

        public static Result<User> Create(string name, string userName,string password, DateTime createdAt)
        { 
            // Name validation
            if (string.IsNullOrWhiteSpace(name))
                return Result<User>.Fail("Name cannot be empty.");
            if (name.Length < MinNameLength || name.Length > MaxNameLength)
                return Result<User>.Fail($"Name must be between {MinNameLength} and {MaxNameLength} characters.");

            // Password validation
            if (password.Length < MinPasswordLength || password.Length > MaxPasswordLength)
                return Result<User>.Fail($"Password must be between {MinPasswordLength} and {MaxPasswordLength} characters.");
            if (string.IsNullOrWhiteSpace(password))
                return Result<User>.Fail("Password cannot be empty.");


            var user = new User(0, name, userName, password, createdAt);
            return Result<User>.Ok(user);
        } 
    }
}
