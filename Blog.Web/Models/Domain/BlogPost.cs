namespace Blog.Web.Models.Domain
{
    public class BlogPost
    {
        public Guid ID { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string BlogUserId { get; set; }
        public BlogUser BlogUser { get; set; }
        public bool Visible { get; set; }

        //Navigation Properties
        public ICollection<Tag> Tags { get; set; }

        public ICollection<BlogPostLike>? Likes { get; set; }
        public ICollection<BlogPostComment>? Comments { get; set; }
    }
}
