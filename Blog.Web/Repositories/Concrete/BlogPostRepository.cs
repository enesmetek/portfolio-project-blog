using Blog.Web.Models.Domain;
using Blog.Web.Repositories.Abstract;

namespace Blog.Web.Repositories.Concrete
{
    public class BlogPostRepository : BaseRepository<BlogPost>, IBlogPostRepository
    {

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
