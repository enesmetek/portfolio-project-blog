using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories.Abstract
{
    public interface IBlogPostCommentRepository : IBaseRepository<BlogPostComment>
    {


        Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIDAsync(Guid blogPostID);
    }
}
