using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories.Concrete
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogDbContext _blogDbContext;

        public TagRepository(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _blogDbContext.Tags.ToListAsync();
        }
        public Task<Tag?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await _blogDbContext.Tags.AddAsync(tag);
            await _blogDbContext.SaveChangesAsync();
            return tag;
        }
        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await _blogDbContext.Tags.FindAsync(tag.ID);

            if(existingTag != null)
            {
                existingTag.ID = tag.ID;
                existingTag.Name = tag.Name;
                await _blogDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;

        }
        public Task<Tag?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
