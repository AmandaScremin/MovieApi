using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Data.Dtos
{
    public class UpdateCinemaDto
    {
        [Required(ErrorMessage = "The name field is required")]
        public string Name { get; set; }
    }
}
