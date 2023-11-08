using Blog.Web.Data;
using Blog.Web.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogDbContext authDbContext;

        public UserRepository(BlogDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users = await authDbContext.Users.ToListAsync();

            var superAdminUser = await authDbContext.Users.FirstOrDefaultAsync(x => x.Email == "superadmin@bloggie.com");

            if (superAdminUser != null)
            {
                users.Remove(superAdminUser);
            }

            return users;
        }
    }
}
