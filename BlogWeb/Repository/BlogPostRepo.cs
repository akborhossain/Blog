using BlogWeb.Data;
using BlogWeb.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BlogWeb.Repository
{
    public class BlogPostRepo : IBlogPost
    {
        private readonly BlogDbContext context;

        public BlogPostRepo(BlogDbContext context)
        {
            this.context = context;
        }
        public async Task<BlogPost> AddAsync(BlogPost post)
        {
            await context.BlogPosts.AddAsync(post);
            await context.SaveChangesAsync();
            return post;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var exPost = await context.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.Id==id);
            if(exPost != null)
            {
                context.BlogPosts.Remove(exPost);
                await context.SaveChangesAsync();
                return exPost;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
           var posts= await context.BlogPosts.Include(x=>x.Tags).ToListAsync();
            return posts;
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return await context.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await context.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost post)
        {
            var existPost= await context.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x=>x.Id == post.Id);
            if (existPost != null) 
            { 
                existPost.Id = post.Id;
                existPost.Heading = post.Heading;
                existPost.PageTitle = post.PageTitle;
                existPost.Content = post.Content;
                existPost.ShortDescription = post.ShortDescription;
                existPost.FeaturedImageUrl = post.FeaturedImageUrl;
                existPost.UrlHandle = post.UrlHandle;
                existPost.PublishedDate = post.PublishedDate;
                existPost.CreatedBy = post.CreatedBy;
                existPost.Visible = post.Visible;
                existPost.UpdatedAt = post.UpdatedAt;
                existPost.UpdatedBy = post.UpdatedBy;
                existPost.Tags = post.Tags;
                await context.SaveChangesAsync();
                return existPost;
            }
            return null;
        }
    }
}
