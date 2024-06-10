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
    public class PostReactionService : IPostReactionService
    {
        private readonly ChatDbContext _dbContext;

        public PostReactionService(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PostReaction> GetReactionByPostAndUser(Guid postId, Guid userId)
        {
            return await _dbContext.PostReactions
                .FirstOrDefaultAsync(r => r.PostId == postId && r.UserId == userId);
        }

        public async Task AddReactionAsync(PostReaction reaction)
        {
            await _dbContext.PostReactions.AddAsync(reaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateReactionAsync(PostReaction reaction)
        {
            _dbContext.PostReactions.Update(reaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<PostReaction>> GetReactionListAsync()
        {
            return await _dbContext.PostReactions.ToListAsync();
        }
    }
}
