using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Data.Dtos
{
    public class CreateMovieDto
    {
        [Required(ErrorMessage = "The Title is required")]
        [MaxLength(200, ErrorMessage = "The title size must be less than 200 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Genre is required")]
        [MaxLength(50, ErrorMessage = "The genre size must be less than 50 characters")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "The Duration is required")]
        [Range(70, 600, ErrorMessage = "The Duration must be between 70 and 600 minutes")]
        public int Duration { get; set; }
    }
}
