using Blog.Web.Models.Domain;
using Blog.Web.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories.Concrete
{
    public class BlogPostCommentRepository : BaseRepository<BlogPostComment>, IBlogPostCommentRepository
    {


        public async Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIDAsync(Guid blogPostID)
        {
            return await base.dbContext.BlogPostComment.Where(x => x.BlogPostID == blogPostID).ToListAsync();
        }
    }
}
