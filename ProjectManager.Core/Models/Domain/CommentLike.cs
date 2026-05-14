using ProjectManager.Core.Models.Common;
using ProjectManager.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.Core.Models.Domain
{
    public class CommentLike : IEntity
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        private CommentLike(int id, int commentId, int userId)
        {
            Id = id;
            CommentId = commentId;
            UserId = userId;
        }
        public static Result<CommentLike> Create(int commentId, int userId)
        {
            var like = new CommentLike(0, commentId, userId);
            return Result<CommentLike>.Ok(like);
        }
    }
}
