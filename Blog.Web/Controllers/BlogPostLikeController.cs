using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository blogPostLikeRepository;

        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            this.blogPostLikeRepository = blogPostLikeRepository;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = new BlogPostLike
            {
                BlogPostID = addLikeRequest.BlogPostID,
                BlogUserId = userId
            };

            await blogPostLikeRepository.AddLikeForBlog(model);
            return Ok();
        }

        [HttpGet]
        [Route("{blogPostID:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesForBlog([FromRoute] Guid blogPostID)
        {
            var totalLikes = await blogPostLikeRepository.GetTotalLikes(blogPostID);
            return Ok(totalLikes);
        }
    }
}