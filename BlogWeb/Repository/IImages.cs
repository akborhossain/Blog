namespace BlogWeb.Repository
{
    public interface IImages
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
