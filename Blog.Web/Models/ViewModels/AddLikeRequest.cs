namespace Blog.Web.Models.ViewModels
{
    public class AddLikeRequest
    {
        public Guid BlogPostID { get; set; }
        public Guid UserID { get; set; }
    }
}
