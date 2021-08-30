using MovieAPI.Entities;
using MovieAPI.Infrastructure.Interfaces;
using MovieAPI.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace MovieAPI.Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public MovieService(MovieContext dbContext, IMapper mapper, ILogger<MovieService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<MovieDto> GetMoviebyId(int id)
        {
            var movie = _dbContext.Movies.AsQueryable().FirstOrDefault(x => x.Id == id);
            var moviedto = _mapper.Map<MovieDto>(movie);

            return moviedto;
        }

        public async Task<IEnumerable<MovieDto>> GetMovies()
        {
            var movies = _dbContext.Movies.AsQueryable().ToList();
            var moviesdto = _mapper.Map<List<MovieDto>>(movies);

            return moviesdto;
        }


        public async Task<MovieDto> AddMovie(CreateOrEditMovieDto dto)
        {
            var movie = _mapper.Map<Movie>(dto);
            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();

            var mobieDto = _mapper.Map<MovieDto>(movie);

            return mobieDto;
        }


        public async Task<bool> DeleteMovie(int id)
        {
            var movie = _dbContext.Movies.FirstOrDefault(x => x.Id == id);

            if (movie is null)
            {
                return false;
            }

            _dbContext.Movies.Remove(movie);
            _dbContext.SaveChanges();

            return true;
        }

        public async Task<MovieDto> UpdateMovie(CreateOrEditMovieDto dto, int id)
        {
            var movie = _dbContext.Movies.FirstOrDefault(x => x.Id == id);

            if (movie is null)
            {
                return null;
            }

            movie.Drirector = dto.Drirector;
            movie.Title = dto.Title;
            _dbContext.SaveChanges();

            var moviedto = _mapper.Map<MovieDto>(movie);

            return moviedto;
        }




    }
}
