using Microsoft.AspNetCore.Identity;

namespace Blog.Web.Repositories.Abstract
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
