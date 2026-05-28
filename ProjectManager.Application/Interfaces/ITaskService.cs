using ProjectManager.Application.DTO.Request;
using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Application.Interfaces
{
    public interface ITaskService
    {
        ProjectTask? Get(int id);
        Result<ProjectTask> Create(CreateTaskRequest request, int ownerId, int projectId);
        Result<List<ProjectTask>> GetAll(int userId);
        Result<Comment> AddComment(int taskId, int userId, CreateCommentRequest request);
        Result<List<Comment>> GetTaskComments(int taskId);
        Result<CommentLike> AddLikeToComment(int commentId, int userId);
        Result<Comment> GetCommentById(int commentId);
    }
}
