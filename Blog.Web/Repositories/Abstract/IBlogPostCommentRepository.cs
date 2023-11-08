using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories.Abstract
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);

        Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIDAsync(Guid blogPostID);
    }
}
