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
  
        public int Id { get; }
        public string Name { get; } = string.Empty;
        public string Password { get; }
        public DateTime? CreatedAt { get; }
        public List<ProjectMember> ProjectMembers { get; } = null!;

        private User (int id, string name, string password, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Password = password;
            CreatedAt = createdAt;
        }

        public static Result<User> Create(string name, string password, DateTime createdAt)
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


            var user = new User(0, name, password, createdAt);
            return Result<User>.Ok(user);
        } 
    }
}
