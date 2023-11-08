using Blog.Web.Models.Domain;
using Blog.Web.Repositories.Abstract;

namespace Blog.Web.Repositories.Concrete
{
    public class BlogPostRepository : BaseRepository<BlogPost>, IBlogPostRepository
    {

    }
}
