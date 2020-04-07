using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Movies.API.Context;
using Movies.API.DTOs;
using Movies.API.Entities;
using Movies.API.Models;
using Movies.API.Repositories;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesRepository moviesRepository;
        private readonly IMapper mapper;
        private readonly ILogger<MoviesController> logger;
        private readonly MoviesDBcontext _context;

        public MoviesController(IMoviesRepository moviesRepository, IMapper mapper, ILogger<MoviesController> logger, MoviesDBcontext context)
        {
            this.moviesRepository = moviesRepository;
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger;
            this._context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public ActionResult<IEnumerable<Models.Movie>> GetMovies()
        {
            logger.LogDebug("Hey my first log");
            var moviesEntity = moviesRepository.GetAll();
            return Ok(mapper.Map<IEnumerable<Models.Movie>>(moviesEntity));
        }

        [HttpGet("{movieId}",Name ="GetMovie")]
        public ActionResult<Models.Movie> GetMovie(int movieId)
        {
            var movieEntity = moviesRepository.Get(movieId);

            if (movieEntity == null)
                return NotFound();

            return Ok(mapper.Map<Models.Movie>(movieEntity));
        }

        [HttpPost]
        public IActionResult CreateMovie([FromBody] MovieDTO movieDTOToCreate)
        {
            if (movieDTOToCreate == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            var movieEntity = mapper.Map<Entities.Movie>(movieDTOToCreate);
            moviesRepository.AddMovie(movieEntity);

            moviesRepository.Save();

            //to get director
            moviesRepository.Get(movieEntity.ID);

            return CreatedAtRoute("GetMovie", new { movieId = movieEntity.ID }, mapper.Map<Models.Movie>(movieEntity));
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        [NonAction]
        public async Task<IActionResult> GetMovieOld([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // PUT: api/Movies/5
        [HttpPut("{id}")]
        [NonAction]

        public async Task<IActionResult> PutMovie([FromRoute] int id, [FromBody] Entities.Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movie.ID)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        [NonAction]

        public async Task<IActionResult> PostMovie([FromBody] Entities.Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.ID }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        [NonAction]

        public async Task<IActionResult> DeleteMovie([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return Ok(movie);
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.ID == id);
        }
    }
}