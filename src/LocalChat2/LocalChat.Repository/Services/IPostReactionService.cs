using LocalChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository.Services
{
    public interface IPostReactionService
    {
        Task<PostReaction> GetReactionByPostAndUser(Guid postId, Guid userId);
        Task AddReactionAsync(PostReaction reaction);
        Task UpdateReactionAsync(PostReaction reaction);
        Task<ICollection<PostReaction>> GetReactionListAsync();
    }
}
