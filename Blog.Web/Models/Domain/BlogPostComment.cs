namespace Blog.Web.Models.Domain
{
    public class BlogPostComment
    {
        public Guid ID { get; set; }
        public string Description { get; set; }
        public Guid BlogPostID { get; set; }
        public string BlogUserId { get; set; }
        public BlogUser BlogUser { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
