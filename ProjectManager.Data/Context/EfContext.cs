using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Data.Context
{
    public class EfContext : DbContext
    {

        public EfContext(DbContextOptions<EfContext> options) : base(options)
        {
        }

        public DbSet<Core.Models.Domain.Project> Projects { get; set; }
        public DbSet<Core.Models.Domain.User> Users { get; set; }
        public DbSet<Core.Models.Domain.ProjectTask> Tasks { get; set; }
        public DbSet<Core.Models.Domain.TaskMember> TaskMembers { get; set; }
        public DbSet<Core.Models.Domain.ProjectMember> ProjectMembers { get; set; }
        public DbSet<Core.Models.Domain.Comment> Comments { get; set; }
        public DbSet<Core.Models.Domain.CommentLike> CommentLikes { get; set; }


    }
}
