namespace Blog.Web.Models.Domain
{
    public class BlogPostLike
    {
        public Guid ID { get; set; }
        public Guid BlogPostID { get; set; }
        public Guid UserID { get; set; }
    }
}
