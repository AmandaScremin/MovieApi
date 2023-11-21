using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Data;
using MovieAPI.Data.Dtos;
using MovieAPI.Models;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MovieController : ControllerBase
    {
        private readonly MovieContext _movieContext;
        private readonly IMapper _mapper;
        public MovieController(MovieContext context, IMapper mapper)
        {
            _movieContext = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Add a new movie to the database
        /// </summary>
        /// <param name="movieDto">Object that contains the needed values for the request</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Created</response>
        [HttpPost]
        public IActionResult AddMovie([FromBody] CreateMovieDto movieDto)
        {
            Movie movie = _mapper.Map<Movie>(movieDto);
            _movieContext.Movies.Add(movie);
            _movieContext.SaveChanges();
            return CreatedAtAction(nameof(GetMovieById), new {id = movie.Id}, movie);
        }

        /// <summary>
        /// Get all the movies from the database
        /// </summary>
        /// <param name="skip">skip this many movies</param>
        /// <param name="take">take this many movies</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Ok</response>
        [HttpGet]
        public IEnumerable<ReadMovieDto> GetMovies([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return _mapper.Map<List<ReadMovieDto>>(_movieContext.Movies.Skip(skip).Take(take).ToList());
        }

        /// <summary>
        /// Get a specific movie from the database
        /// </summary>
        /// <param name="id">movie id</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Ok</response>
        [HttpGet]
        public IActionResult GetMovieById(int id)
        {
            var movie = _movieContext.Movies.FirstOrDefault(x => x.Id == id);
            if (movie == null) return NotFound();
            return Ok(movie);
        }

        /// <summary>
        /// Get a specific movie
        /// </summary>
        /// <param name="id">movie id</param>
        /// <param name="updateMovieDto">Object that constains the needed values fot the request</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">NoContent</response>
        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieDto updateMovieDto)
        {
            var movie = _movieContext.Movies.FirstOrDefault(movie => movie.Id == id);

            if (movie == null) return NotFound();

            _mapper.Map(updateMovieDto, movie);
            _movieContext.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Update a movie partially
        /// </summary>
        /// <param name="id">movie id</param>
        /// <param name="patch">Object that constains the needed values fot the request</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">NoContent</response>
        [HttpPatch("{id}")]
        public IActionResult UpdateMoviePartially(int id, JsonPatchDocument<UpdateMovieDto> patch)
        {
            var movie = _movieContext.Movies.FirstOrDefault(movie => movie.Id == id);

            if (movie == null) return NotFound();

            var movieToUpdate = _mapper.Map<UpdateMovieDto>(movie);
            patch.ApplyTo(movieToUpdate, ModelState);

            if (!TryValidateModel(movieToUpdate))
            {
                ValidationProblem(ModelState);
            }

            _mapper.Map(movieToUpdate, movie);
            _movieContext.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Delete a movie
        /// </summary>
        /// <param name="id">movie id</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">NoContent</response>
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var movie = _movieContext.Movies.FirstOrDefault(movie=> movie.Id == id);
            if (movie == null) return NotFound();

            _movieContext.Movies.Remove(movie);
            _movieContext.SaveChanges();

            return NoContent();
        }
    }
}
