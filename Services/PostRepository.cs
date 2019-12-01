using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetBook.Domains;
using TweetBook.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TweetBook.Services
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext context;

        public PostRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreatePost(Post post)
        {
            await context.Posts.AddAsync(post);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePost(Guid postId)
        {
            var post= await context.FindAsync<Post>(postId);
            if (post!=null)
            {
                context.Remove(post);
            }
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<Post> GetPost(Guid postId)
        {
            var post = await context.FindAsync<Post>(postId);
            if (post!=null)
            {
                return post;
            }
            return null;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await context.Posts.ToListAsync();
        }

        public async Task<bool> UpdatePost(Post post)
        {
            var postupdate = context.Attach(post);
            postupdate.State= EntityState.Modified;
            return await context.SaveChangesAsync() > 0;
        }
    }
}