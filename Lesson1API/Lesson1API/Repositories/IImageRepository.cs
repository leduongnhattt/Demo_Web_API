using Lesson1API.Models.Domain;

namespace Lesson1API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
