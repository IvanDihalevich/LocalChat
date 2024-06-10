using LocalChat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalChat.Repository.Services
{
    public interface IPostService
    {
        Task AddPostAsync(Post post);
        Post GetPostById(Guid postId);
        Task<IEnumerable<Post>> GetAllPostsById();
        void UpdatePost(Guid postId);
        void DeleteMessage(Guid id);
    }
}
