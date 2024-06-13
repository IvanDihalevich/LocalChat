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
    public class PostService : IPostService
    {
        private readonly ChatDbContext _dbContext;

        public PostService(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task AddPostAsync(Post post)
        {
            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();
        }

        public Post GetPostById(Guid postId)    
        {
            return _dbContext.Posts.Include(p => p.Sender).FirstOrDefault(m => m.Id == postId);
        }

        public async Task<IEnumerable<Post>> GetAllPostsById()
        {
            return await _dbContext.Posts.ToListAsync();
        }

        public void UpdatePost(Guid postId)
        {
            var existingPost = _dbContext.Posts.FirstOrDefault(m=>m.Id == postId);
            _dbContext.Posts.Update(existingPost);
                _dbContext.SaveChanges();
        }

        public void DeleteMessage(Guid id)
        {
            var postToDelete = _dbContext.Posts
                                        .Include(p => p.Comments) // Include comments related to the post
                                        .Include(p => p.PostReactions) // Include reactions related to the post
                                        .FirstOrDefault(p => p.Id == id);

            if (postToDelete != null)
                _dbContext.Comments.RemoveRange(postToDelete.Comments);
                _dbContext.PostReactions.RemoveRange(postToDelete.PostReactions);
                _dbContext.Posts.Remove(postToDelete);
                _dbContext.SaveChanges();
            }
        }
}
