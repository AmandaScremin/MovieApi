using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Data;
using MovieAPI.Data.Dtos;
using MovieAPI.Models;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private readonly MovieContext _movieContext;
        private readonly IMapper _mapper;

        public CinemaController(MovieContext movieContext, IMapper mapper)
        {
            _movieContext = movieContext;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddCinema([FromBody] CreateCinemaDto createCinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(createCinemaDto);
            _movieContext.Cinema.Add(cinema);
            _movieContext.SaveChanges();
            return CreatedAtAction(nameof(GetCinemaById), new { Id = cinema.Id}, createCinemaDto);
        }
        [HttpGet]
        public IActionResult GetCinemas()
        {
            var cinema=_movieContext.Cinema.ToList();

            return Ok(cinema);
        }

        [HttpGet("{id}")]
        public IActionResult GetCinemaById(int id)
        {
            var cinema = _mapper.Map<List<ReadCinemaDto>>(_movieContext.Cinema.FirstOrDefault(cinema => cinema.Id == id));
            if (cinema != null)
            {
                ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
                return Ok(cinemaDto);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            var cinema = _mapper.Map<List<ReadCinemaDto>>(_movieContext.Cinema.FirstOrDefault(cinema => cinema.Id == id));
            if (cinema == null)
            {
                return NotFound();
            }
            _mapper.Map(cinemaDto, cinema);
            _movieContext.SaveChanges();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCinema(int id)
        {
            var cinema = _movieContext.Cinema.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema != null)
            {
                _movieContext.Cinema.Remove(cinema);
                _movieContext.SaveChanges();
                return Ok(cinema);
            }
            return NotFound();
            
        }

    }
}
