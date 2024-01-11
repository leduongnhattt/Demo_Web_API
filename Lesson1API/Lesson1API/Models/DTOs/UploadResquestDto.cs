using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson1API.Models.DTOs
{
    public class UploadResquestDto
    {

        [Required] 
        public IFormFile File { get; set; }

        [Required]
        public string? FileName { get; set; }

        [Required]
        public string? FileDescription { get; set; }

    }
}
