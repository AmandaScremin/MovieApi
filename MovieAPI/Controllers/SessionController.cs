using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Data;
using MovieAPI.Data.Dtos;
using MovieAPI.Models;

namespace MovieAPI.Controllers
{
    public class SessionController : ControllerBase
    {
        private readonly MovieContext _movieContext;
        private readonly IMapper _mapper;

        public SessionController(MovieContext movieContext, IMapper mapper)
        {
            _movieContext = movieContext;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddSession(CreateSessionDto createSessionDto)
        {
            Session session = _mapper.Map<Session>(createSessionDto);
            _movieContext.Sessions.Add(session);
            _movieContext.SaveChanges();
            return CreatedAtAction(nameof(GetSessionById), new { movieId = session.MovieId, cinemaId = session.CinemaId }, session);
        }

        [HttpGet]
        public IEnumerable<ReadSessionDto> GetSessions()
        {
            return _mapper.Map<List<ReadSessionDto>>(_movieContext.Sessions.ToList());
        }

        [HttpGet("{movieId}/{cinemaId}")]
        public IActionResult GetSessionById(int movieId, int cinemaId)
        {
            Session session = _movieContext.Sessions.FirstOrDefault(r => r.MovieId == movieId && r.CinemaId == cinemaId);
            if (session == null)
            {
                ReadSessionDto sessionDto = _mapper.Map<ReadSessionDto>(session);
                return Ok();
            }
            return NotFound();
        }
    }
}
