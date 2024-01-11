using System.ComponentModel.DataAnnotations;

namespace Lesson1API.Models.DTOs
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a minimum of 3 character")]
        [MaxLength(30, ErrorMessage = "Code has to be a maximum of 30 character")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 character")]
        public string Name { get; set; }
        public string? RegionImageUrl {  get; set; }  
    }
}
