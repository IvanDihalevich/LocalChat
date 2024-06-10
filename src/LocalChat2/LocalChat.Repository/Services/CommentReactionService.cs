using LocalChat.Core.Context;
using LocalChat.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository.Services
{
    public class CommentReactionService : ICommentReactionService
    {
        private readonly ChatDbContext _dbContext;

        public CommentReactionService(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommentReaction> GetReactionByCommentAndUser(Guid commentId, Guid userId)
        {
            return await _dbContext.CommentReactions
                .FirstOrDefaultAsync(r => r.CommentId == commentId && r.UserId == userId);
        }

        public async Task AddReactionAsync(CommentReaction reaction)
        {
            await _dbContext.CommentReactions.AddAsync(reaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateReactionAsync(CommentReaction reaction)
        {
            _dbContext.CommentReactions.Update(reaction);
            await _dbContext.SaveChangesAsync();
        }
    }
}
