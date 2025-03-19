using Azure;
using BlogWeb.Data;
using BlogWeb.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogWeb.Repository
{
    public class TagRepo : ITag
    {
        private readonly BlogDbContext context;

        public TagRepo(BlogDbContext context)
        {
            this.context = context;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await context.Tags.AddAsync(tag);
            await context.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var tag1 = await context.Tags.FirstOrDefaultAsync(x => x.Id == id);
            if (tag1 != null)
            {
                context.Tags.Remove(tag1);
                await context.SaveChangesAsync();
                return tag1;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var tags = await context.Tags.ToListAsync();
            return tags;
        }

        public async Task<Tag?> GetByIdAsync(Guid id)
        {
           var tag= await context.Tags.FirstOrDefaultAsync(x => x.Id == id);
           return tag;
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var tag1 = await context.Tags.FirstOrDefaultAsync(x => x.Id == tag.Id);
            if (tag1 != null)
            {

                tag1.Name = tag.Name;
                tag1.DisplayName = tag.DisplayName;
                await context.SaveChangesAsync();
                return tag1;
                
            }
            return null;
        }
    }
}
