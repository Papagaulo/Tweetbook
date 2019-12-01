using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using TweetBook.Domains;

namespace TweetBook.Services
{
    public interface IPostRepository{
        Task<Post> GetPost(Guid postId);
        Task<IEnumerable<Post>> GetPosts();
        Task<bool> CreatePost(Post post); 
        Task<bool> DeletePost(Guid postId); 
        Task<bool> UpdatePost(Post post); 

    }
}