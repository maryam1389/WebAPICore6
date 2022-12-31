using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class PointOfInterestForUpdateDTO
    {

        [Required(ErrorMessage = "lotfan nam ra vared konid")]
        [MaxLength(50, ErrorMessage = "")]

        public string Name { get; set; } = string.Empty;

        [MaxLength(200, ErrorMessage = "")]
        public string? Description { get; set; }
    }
}
