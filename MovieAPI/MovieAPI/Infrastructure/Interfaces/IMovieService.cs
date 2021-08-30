using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieAPI.DTOModels;


namespace MovieAPI.Infrastructure.Interfaces
{
    public interface IMovieService
    {
        Task<MovieDto> GetMoviebyId(int id);
        Task<IEnumerable<MovieDto>> GetMovies();
        Task<MovieDto> AddMovie(CreateOrEditMovieDto dto);
        Task<bool> DeleteMovie(int id);
        Task<MovieDto> UpdateMovie(CreateOrEditMovieDto dto, int id);
    }
}
