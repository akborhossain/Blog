using BlogWeb.Models.Entities;

namespace BlogWeb.Repository
{
    public interface IBlogPost
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetByIdAsync(Guid id);
        Task<BlogPost?> GetByUrlHandleAsync(string urlHandle);
        Task<BlogPost> AddAsync(BlogPost post);
        Task<BlogPost?> UpdateAsync(BlogPost post);
        Task<BlogPost?> DeleteAsync(Guid id);
    }
}
