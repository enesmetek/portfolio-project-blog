namespace Blog.Web.Repositories.Abstract
{
    public interface IImageRepository
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
