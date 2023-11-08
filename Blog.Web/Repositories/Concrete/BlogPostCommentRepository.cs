﻿using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories.Concrete
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly BlogDbContext blogDbContext;

        public BlogPostCommentRepository(BlogDbContext blogDbContext)
        {
            this.blogDbContext = blogDbContext;
        }

        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
            await blogDbContext.BlogPostComment.AddAsync(blogPostComment);
            await blogDbContext.SaveChangesAsync();
            return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIDAsync(Guid blogPostID)
        {
            return await blogDbContext.BlogPostComment.Where(x => x.BlogPostID == blogPostID).ToListAsync();
        }
    }
}
