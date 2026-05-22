using Microsoft.EntityFrameworkCore;
using ProjectManager.Core.Models.Domain;
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectTask>()
                .HasOne(pt => pt.Owner)
                .WithMany(o => o.ProjectTasks)
                .HasForeignKey(pt => pt.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectTask>()
                .HasOne(pt => pt.Project)
                .WithMany(p => p.ProjectTasks)
                .HasForeignKey(p => p.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectMember>()
                .HasOne(pm => pm.User)
                .WithMany(u => u.ProjectMembers)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<ProjectMember>()
                .HasOne(pm => pm.Project)
                .WithMany(p => p.ProjectMembers)
                .OnDelete(DeleteBehavior.Cascade);
        }


    }
}
