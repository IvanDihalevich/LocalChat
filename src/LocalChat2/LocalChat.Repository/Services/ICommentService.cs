using LocalChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository.Services
{
	public interface ICommentService
	{
		Task AddCommentAsync(Comment comment);
		Comment GetCommentById(Guid commentId);
		Task<IEnumerable<Comment>> GetAllCommentsById();
		Task<IEnumerable<Comment>> GetCommentsByPost(Guid postId);
		void UpdateComment(Guid commentId);
		void DeleteComment(Guid id);
	}
}
