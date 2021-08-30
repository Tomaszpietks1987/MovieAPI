using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using MovieAPI.Controllers;
using MovieAPI.DTOModels;
using MovieAPI.Infrastructure.Interfaces;

using NSubstitute;

using System;
using System.Threading.Tasks;

using Xunit;

namespace XUnitTestProject1
{
    public class MovieControllerTest
    {
        [Fact]
        public async void Get_Movie_ReturnsNotFoundResult_WhenMovieIsNotFound()
        {
            // Arrange
            var movieService = Substitute.For<IMovieService>();
            var loger = Substitute.For<ILogger<MovieController>>();
            var movieController = new MovieController(movieService, loger);
            var id = 1000000;
            movieService.GetMoviebyId(id).Returns(Task.FromResult<MovieDto>(null));

            // Act
            var result = await movieController.GetMovie(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void Get_Movie_ReturnsOkResult_WhenMovieIsFound()
        {
            // Arrange
            var movieService = Substitute.For<IMovieService>();
            var loger = Substitute.For<ILogger<MovieController>>();
            var movieController = new MovieController(movieService, loger);

            var movie = new MovieDto()
            {
                Id = 5,
                Drirector = "John",
                Title = "Rambo"
            };

            movieService.GetMoviebyId(movie.Id).Returns(Task.FromResult<MovieDto>(movie));

            // Act
            var result = await movieController.GetMovie(movie.Id);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }



        [Fact]
        public async void Get_Movie_ReturnsCreatedResult_WhenMovieAreCreated()
        {
            // Arrange
            var movieService = Substitute.For<IMovieService>();
            var loger = Substitute.For<ILogger<MovieController>>();
            var movieController = new MovieController(movieService, loger);

            var movie = new CreateOrEditMovieDto()
            {
                Drirector = "John",
                Title = "Rambo"
            };

            movieService.AddMovie(movie).Returns(Task.FromResult<MovieDto>(null));

            // Act
            var result = await movieController.CreateMovie(movie);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

    }
}
