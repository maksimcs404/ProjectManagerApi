using Microsoft.EntityFrameworkCore;
using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Domain;
using ProjectManager.Core.Models.Interfaces.Repositories;
using ProjectManager.Data.Context;
using ProjectManager.Data.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Data.Repositories

{
    //TODO: Доделать
    public class TaskRepository : BaseRepository<ProjectTask>, ITaskRepository
    {
        private readonly DbSet<TaskMember> _taskMembers;
        private readonly DbSet<Comment> _comments;
        private readonly DbSet<CommentLike> _commentLikes;
        public TaskRepository(EfContext context) : base(context)
        {
            _taskMembers = _context.Set<TaskMember>();
            _comments = _context.Set<Comment>();
            _commentLikes = _context.Set<CommentLike>();
        }
        public Result<List<ProjectTask>> GetAllOwnTasksByUserId(int userId)
        {
            try
            {
                var list = _dbSet.Where(p => p.OwnerId == userId).ToList();
                return Result<List<ProjectTask>>.Ok(list);
            }
            catch (Exception ex)
            {

                return Result<List<ProjectTask>>.Fail(ex.Message);
            }        
        }
        public Result<List<ProjectTask>> GetAllOtherTasksByUserId(int userId)
        {
            try
            {
                var tasks =  _taskMembers
                    .Where(u => u.UserId == userId)
                    .Select(u => u.Task)
                    .ToList();

                return Result<List<ProjectTask>>.Ok(tasks);
            } catch (Exception ex)
            {
                return Result<List<ProjectTask>>.Fail($"{ex.Message}");
            }
        }

        public Result<Comment> AddComment(int taskId, int userId, string data, string title)
        {
            try
            {
                var task = _dbSet.FirstOrDefault(t => t.Id == taskId);
                if (task == null)
                    return Result<Comment>.Fail("Task not found.");

                var commentResult = Comment.Create(data, title, taskId, userId);
                if (!commentResult.IsSuccess)
                    return Result<Comment>.Fail(commentResult.Error!);

                _comments.Add(commentResult.Data!);
                _context.SaveChanges();
                return Result<Comment>.Ok(commentResult.Data!);
            }
            catch (Exception ex)
            {
                return Result<Comment>.Fail(ex.Message);
            }
        }

        public Result<List<Comment>> GetTaskComments(int taskId)
        {
            try
            {
                var task = _dbSet.FirstOrDefault(t => t.Id == taskId);
                if (task == null)
                    return Result<List<Comment>>.Fail("Task not found.");

                var comments = _comments.Where(c => c.TaskId == taskId).ToList();
                return Result<List<Comment>>.Ok(comments);
            }
            catch (Exception ex)
            {
                return Result<List<Comment>>.Fail(ex.Message);
            }
        }

        public Result<CommentLike> AddLikeToComment(int commentId, int userId)
        {
            try
            {
                var comment = _comments.FirstOrDefault(c => c.Id == commentId);
                if (comment == null)
                    return Result<CommentLike>.Fail("Comment not found.");

                var existedLike = _commentLikes.FirstOrDefault(l => l.CommentId == commentId && l.UserId == userId);
                if (existedLike != null)
                    return Result<CommentLike>.Fail("Like already exists.");

                var likeResult = CommentLike.Create(commentId, userId);
                if (!likeResult.IsSuccess)
                    return Result<CommentLike>.Fail(likeResult.Error!);

                _commentLikes.Add(likeResult.Data!);
                _context.SaveChanges();
                return Result<CommentLike>.Ok(likeResult.Data!);
            }
            catch (Exception ex)
            {
                return Result<CommentLike>.Fail(ex.Message);
            }
        }

        public Result<Comment> GetCommentById(int commentId)
        {
            try
            {
                var comment = _comments.FirstOrDefault(c => c.Id == commentId);
                if (comment == null)
                    return Result<Comment>.Fail("Comment not found.");

                return Result<Comment>.Ok(comment);
            }
            catch (Exception ex)
            {
                return Result<Comment>.Fail(ex.Message);
            }
        }

    }
}
