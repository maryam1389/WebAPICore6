using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class PointOfInterestForCreationDTO
    {
        [Required(ErrorMessage ="")]
        [MaxLength(50,ErrorMessage ="")]

        public string Name { get; set; } = string.Empty;

        [MaxLength(200,ErrorMessage ="")]
        public string? Description { get; set; }

    }
}
