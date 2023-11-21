using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Data.Dtos
{
    public class ReadMovieDto
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; }
        public DateTime HoraConsulta { get; set; } = DateTime.Now;
        public ICollection<ReadSessionDto> Sessions { get; set; }
    }
}
