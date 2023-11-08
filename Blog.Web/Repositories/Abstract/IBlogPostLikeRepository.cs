using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories.Abstract
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikes(Guid blogPostID);
        Task<IEnumerable<BlogPostLike>>GetLikesForBlog(Guid blogPostID);
        Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);
    }
}
