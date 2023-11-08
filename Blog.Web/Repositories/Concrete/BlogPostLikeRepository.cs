using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories.Concrete
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BlogDbContext blogDbContext;

        public BlogPostLikeRepository(BlogDbContext blogDbContext)
        {
            this.blogDbContext = blogDbContext;
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await blogDbContext.BlogPostLike.AddAsync(blogPostLike);
            await blogDbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostID)
        {
            return await blogDbContext.BlogPostLike.Where(x => x.BlogPostID == blogPostID).ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid blogPostID)
        {
            return await blogDbContext.BlogPostLike.CountAsync(x => x.BlogPostID == blogPostID);
        }
    }
}
