using LocalChat.Core.Context;
using LocalChat.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository.Services
{
	public class CommentService : ICommentService
	{
		private readonly ChatDbContext _dbContext;
        public CommentService(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddCommentAsync(Comment comment)
		{
			await _dbContext.Comments.AddAsync(comment);
			await _dbContext.SaveChangesAsync();
		}

		public void DeleteComment(Guid id)
		{
			var commentToDelete = _dbContext.Comments.FirstOrDefault(m => m.Id == id);
			if (commentToDelete != null)
			{
				_dbContext.Comments.Remove(commentToDelete);
				_dbContext.SaveChanges();
			}
		}

		public async Task<IEnumerable<Comment>> GetAllCommentsById()
		{
			return await _dbContext.Comments.Include(c => c.Sender).ToListAsync();
		}

		public async Task<IEnumerable<Comment>> GetCommentsByPost(Guid postId)
		{
			return (await _dbContext.Comments.Include(c => c.CommentReactions).ToListAsync()).Where(c => c.PostId == postId);
		}

		public Comment GetCommentById(Guid commentId)
		{
			return _dbContext.Comments.FirstOrDefault(c => c.Id == commentId);
		}

		public void UpdateComment(Guid commentId)
		{
			var existingComment = _dbContext.Comments.FirstOrDefault(m => m.Id == commentId);
			_dbContext.Comments.Update(existingComment);
			_dbContext.SaveChanges();
		}
	}
}
