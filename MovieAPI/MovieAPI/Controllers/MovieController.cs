using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieAPI.DTOModels;
using MovieAPI.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ILogger<MovieController> _logger;

        public MovieController(IMovieService movieService, ILogger<MovieController> loger)
        {
            _movieService = movieService;
            _logger = loger;
        }


        /// <summary>
        /// Create list of movies.
        /// </summary>
        /// <returns>Returns list of movies.</returns>
        /// <response code="200">Return all existing movies.</response>
        /// <response code="404">Return 404 eroor if movies don't exist.</response>
        /// <sample>
        ///     GET api/GetAllMovies
        /// </sample>   
        [HttpGet]
        [Route("api/GetAllMovies")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MovieDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetAllMovies()
        {
            _logger.LogInformation("Start Action GetAllMovies");
            _logger.LogInformation("Requesting for all movies.");

            var movies = await _movieService.GetMovies();
            if (movies is null)
            {
                _logger.LogWarning("The movies don't exist.");
                return NotFound();
            }

            _logger.LogInformation("Return all movies.");
            return Ok(movies);
        }


        /// <summary>
        /// Get current Movie
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returnt cureent movie by given id.</returns>
        /// <response code="200">Return movie if movie exist.</response>
        /// <response code="404">Return 404 eroor if movie don't exist.</response>
        /// <sample>
        ///     GET api/GetMovie/4
        /// </sample> 
        [HttpGet]
        [Route("api/GetMovie/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieDto>> GetMovie([FromRoute] int id)
        {
            _logger.LogInformation("Start Action GetMovie");
            _logger.LogInformation("Requesting for current movie.");
            var movie = await _movieService.GetMoviebyId(id);
            if (movie is null)
            {
                _logger.LogWarning("The movie don't exist.");
                return NotFound();
            }

            _logger.LogInformation("Return current movie.");
            return Ok(movie);
        }


        /// <summary>
        /// Create new movie by given values.
        /// </summary>
        /// <param name="MovieDto"></param>
        /// <response code="201">Return movie if movie is created..</response>
        /// <response code="204">Return 400 error if movie is not created.</response>
        /// <sample>
        ///  api/CreateMovie
        ///     {
        ///         "drirector": "string",
        ///         "title": "string"
        ///     }
        /// </sample>
        [HttpPost]
        [Route("api/CreateMovie")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MovieDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateMovie([FromBody] CreateOrEditMovieDto dto)
        {
            _logger.LogInformation("Start Action GetMovie");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("The movie model is not valid");
                return BadRequest(ModelState);
            }
            try
            {
                var movie = await _movieService.AddMovie(dto);
                if (movie == null)
                {
                    _logger.LogWarning("The movie don't exist.");
                    return NotFound();
                }
                _logger.LogInformation("The movie is created.");

                return Created($"/api/GetMovie/{movie.Id}", movie);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Application throw new Exception: {ex.Message}");
                return ValidationProblem(ex.Message);
            }
        }


        /// <summary>
        /// Edit current movie by given id.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Return movie if movie is edit.</response>
        /// <response code="400">Return 400 if is BedRequest</response>
        /// <response code="404">Return 404 error if movie dont exist/response>
        ///  <sample>
        ///  api/EditMovie/4
        ///     {
        ///         "drirector": "string",
        ///         "title": "string"
        ///     }
        /// </sample>
        [HttpPut]
        [Route("api/EditMovie/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> EditMovie([FromBody] CreateOrEditMovieDto dto, [FromRoute] int id)
        {
            _logger.LogInformation("Start Action EditMovie");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("The movie model is not valid");
                return ValidationProblem(ModelState);
            }

            try
            {
                _logger.LogInformation("Start edit current movie.");
                var movie = await _movieService.UpdateMovie(dto, id);

                if (movie is null)
                {
                    _logger.LogWarning("The movie don't exist");
                    return NotFound();
                }

                _logger.LogInformation("The movie is edit.");
                return Ok(movie);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Application throw new Exception: {ex.Message}");
                return ValidationProblem(ex.Message);
            }
        }

        /// <summary>
        /// Delete current movie by given id.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Return 204 if is movie wos deleted</response>
        /// <response code="404">Return 404 error if movie dont exist/response>
        ///  <sample>
        ///     api/DeleteMovie/4
        /// </sample>
        [HttpDelete]
        [Route("api/DeleteMovie/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteMovie([FromRoute] int id)
        {
            _logger.LogInformation("Start Action DeleteMovie");
            try
            {
                _logger.LogInformation("Start delete current movie.");
                var isDeleted = await _movieService.DeleteMovie(id);
                if (isDeleted)
                {
                    _logger.LogInformation("The movie is deleted.");
                    return NoContent();
                }

                _logger.LogWarning("The movie don't exist");
                return NotFound();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Application throw new Exception: {ex.Message}");
                return ValidationProblem(ex.Message);
            }
        }

    }
}
