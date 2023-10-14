using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories.Concrete
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogDbContext _blogDbContext;

        public BlogPostRepository(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await _blogDbContext.AddAsync(blogPost);
            await _blogDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var blogPost = await _blogDbContext.BlogPosts.FirstOrDefaultAsync(x => x.ID == id);
            if(blogPost != null)
            {
                _blogDbContext.BlogPosts.Remove(blogPost);
                await _blogDbContext.SaveChangesAsync();
                return blogPost;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            var blogPosts = await _blogDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
            return blogPosts;
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            var blogPost = await _blogDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.ID == id);
            return blogPost;
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await _blogDbContext.BlogPosts.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlog = await _blogDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.ID == blogPost.ID);

            if (existingBlog != null)
            {
                existingBlog.ID = blogPost.ID;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;    
                existingBlog.Content = blogPost.Content;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.Author = blogPost.Author;  
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.Tags = blogPost.Tags;

                await _blogDbContext.SaveChangesAsync();
                return existingBlog;
            }

            return null;
        }
    }
}
