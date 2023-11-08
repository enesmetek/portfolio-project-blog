using Microsoft.AspNetCore.Identity;

namespace Blog.Web.Models.Domain
{
    public class BlogUser : IdentityUser
    {
        public ICollection<BlogPost> Posts { get; set; }
        public ICollection<BlogPostComment> PostComments { get; set; }
        public ICollection<BlogPostLike> Likes { get; set; }


    }
}
