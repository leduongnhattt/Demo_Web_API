using Lesson1API.Models.Domain;
using Lesson1API.Models.DTOs;
using Lesson1API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Lesson1API.Controllers
{
    [ApiController]
    public class ImagesController : Controller
    {
        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        public IImageRepository imageRepository { get; }

        //POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] UploadResquestDto resquestDto)
        {
            ValidateFileUpload(resquestDto);
            if(ModelState.IsValid)
            {

                //Convert DTO to Domain model
                var imageDomainModel = new Image
                {
                    File = resquestDto.File,
                    FileExtension = Path.GetExtension(resquestDto.File.FileName),
                    FileSizeInBytes = resquestDto.File.Length,
                    FileDescription = resquestDto.FileDescription
                };



                //User repository to upload image
                await imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(UploadResquestDto requestDto )
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png"};
            if(!allowedExtensions.Contains(Path.GetExtension(requestDto.FileName))) {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if(requestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File size more than 10MB, please upload a smaller file size");
            }    
        }
    }
}
