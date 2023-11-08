using Blog.Web.Models.Domain;
using Blog.Web.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories.Concrete
{
    public class BlogPostLikeRepository : BaseRepository<BlogPostLike>, IBlogPostLikeRepository
    {


        public BlogPostLikeRepository()
        {

        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await base.dbContext.BlogPostLike.AddAsync(blogPostLike);
            await base.dbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostID)
        {
            return await base.dbContext.BlogPostLike.Where(x => x.BlogPostID == blogPostID).ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid blogPostID)
        {
            return await base.dbContext.BlogPostLike.CountAsync(x => x.BlogPostID == blogPostID);
        }
    }
}
