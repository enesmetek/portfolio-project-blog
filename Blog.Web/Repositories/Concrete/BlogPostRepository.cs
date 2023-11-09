using Blog.Web.Models.Domain;
using Blog.Web.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories.Concrete
{
    public class BlogPostRepository : BaseRepository<BlogPost>, IBlogPostRepository
    {



        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await base.dbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);

        }


    }
}
