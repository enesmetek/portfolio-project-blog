using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        private readonly SignInManager<BlogUser> signInManager;
        private readonly UserManager<BlogUser> userManager;
        private readonly IBlogPostCommentRepository blogPostCommentRepository;

        public BlogsController(IBlogPostRepository blogPostRepository,
            IBlogPostLikeRepository blogPostLikeRepository,
            SignInManager<BlogUser> signInManager,
            UserManager<BlogUser> userManager,
            IBlogPostCommentRepository blogPostCommentRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.blogPostCommentRepository = blogPostCommentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var liked = false;
            var blogPost = await blogPostRepository.GetByUrlHandleAsync(urlHandle);
            var blogDetailsViewModel = new BlogDetailsViewModel();

            if (blogPost != null)
            {
                var totalLikes = await blogPostLikeRepository.GetTotalLikes(blogPost.ID);

                if (signInManager.IsSignedIn(User))
                {

                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    //Get Like for this blog for this user
                    var likesForBlog = await blogPostLikeRepository.GetLikesForBlog(blogPost.ID);

                    var userID = userManager.GetUserId(User);

                    if (userID != null)
                    {
                        var likeFromUser = likesForBlog.FirstOrDefault(x => x.BlogUserId == userId);
                        liked = likeFromUser != null;
                    }
                }

                //Get comments for blog post
                var blogCommentsDomainModel = await blogPostCommentRepository.GetCommentsByBlogIDAsync(blogPost.ID);

                var blogCommentsForView = new List<BlogComment>();

                foreach (var blogComment in blogCommentsDomainModel)
                {
                    blogCommentsForView.Add(new BlogComment
                    {
                        Description = blogComment.Description,
                        DateAdded = blogComment.DateAdded,
                        Username = (await userManager.FindByIdAsync(blogComment.BlogUserId.ToString())).UserName
                    });
                }


                blogDetailsViewModel = new BlogDetailsViewModel
                {
                    ID = blogPost.ID,
                    Content = blogPost.Content,
                    PageTitle = blogPost.PageTitle,
                    //Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    Heading = blogPost.Heading,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                    UrlHandle = blogPost.UrlHandle,
                    Visible = blogPost.Visible,
                    Tags = blogPost.Tags,
                    TotalLikes = totalLikes,
                    Liked = liked,
                    Comments = blogCommentsForView
                };
            }
            return View(blogDetailsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(BlogDetailsViewModel blogDetailsViewModel)
        {
            if (signInManager.IsSignedIn(User))
            {

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var domainModel = new BlogPostComment
                {
                    BlogPostID = blogDetailsViewModel.ID,
                    Description = blogDetailsViewModel.CommentDescription,
                    BlogUserId = userId,
                    DateAdded = DateTime.Now
                };

                await blogPostCommentRepository.AddAsync(domainModel);
                return RedirectToAction("Index", "Home",
                    new { urlHandle = blogDetailsViewModel.UrlHandle });
            }

            return View();
        }
    }
}
