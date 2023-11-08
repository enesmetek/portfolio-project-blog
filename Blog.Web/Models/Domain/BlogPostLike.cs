namespace Blog.Web.Models.Domain
{
    public class BlogPostLike
    {
        public Guid ID { get; set; }
        public Guid BlogPostID { get; set; }
        public string BlogUserId { get; set; }
        public BlogUser BlogUser { get; set; }
    }
}
