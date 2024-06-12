using LocalChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository.Services
{
    public interface ICommentReactionService
    {
        Task<CommentReaction> GetReactionByCommentAndUser(Guid commentId, Guid userId);
        Task AddReactionAsync(CommentReaction reaction);
        Task UpdateReactionAsync(CommentReaction reaction);
    }
}
