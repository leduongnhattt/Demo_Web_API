using Lesson1API.Data;
using Lesson1API.Models.Domain;

namespace Lesson1API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private IWebHostEnvironment webHostEnvironment;
        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, WalksDbContext walksDbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            walksDbContext = walksDbContext;
            httpContextAccessor = httpContextAccessor;
        }

        public IHttpContextAccessor httpContextAccessor { get; }
        public WalksDbContext walksDbContext { get; }

        public async Task<Image> Upload(Image image)

        {   // Kết hợp các chuỗi thành đường dẫn tuyệt đối
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Image", $"{image.FileName}{image.FileExtension}" );


            //Upload Image to local Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            // Add Image to the Images Table
            await walksDbContext.Images.AddAsync(image);
            await walksDbContext.SaveChangesAsync();

            return image;
        }
    }
}
