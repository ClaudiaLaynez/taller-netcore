using Demo.API.Controllers;
using Demo.Application.Interfaces;
using Demo.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Demo.Test
{

    public class MovieControllerTests
    {
        private readonly Mock<ILogger<MovieController>> _loggerMock;
        private readonly Mock<IMovieService> _movieServiceMock;
        private readonly MovieController _controller;
        private List<Movie> _movieList;

        public MovieControllerTests()
        {
            _loggerMock = new Mock<ILogger<MovieController>>(); ;
            _movieServiceMock = new Mock<IMovieService>();
            _controller = new MovieController(_movieServiceMock.Object, _loggerMock.Object);
            _movieList = InitDummyList();
        }

        [Fact]
        public async Task GetAll_Returns_All_Movies()
        {

            // Arrange
            _movieServiceMock.Setup(service => service.GetAll()).ReturnsAsync(_movieList);

            // Act
            var actionResult = await _controller.GetAll();

            // Assert
            var result = Assert.IsType<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<List<Movie>>(result.Value);
            Assert.Equal(5, model.Count);
        }

        [Fact]
        public async Task Get_MovieById()
        {
            //Arrange
            int movieId = 1;
            _movieServiceMock.Setup(s => s.GetById(movieId)).ReturnsAsync(_movieList.Where(x => x.Id == movieId).First);

            //Act
            var actionResult = await _controller.GetMovie(movieId);

            //Assert
            var result = Assert.IsType<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<Movie>(result.Value);
            Assert.Equal(movieId, model.Id);
        }

        [Fact]
        public async Task CreateMovie()
        {

            //Arrange
            Movie movie = new Movie();
            movie.Id = 5;
            movie.Genre = GenreType.Terror;
            movie.Title = "Pelicula 1";
            movie.ReleaseDate = DateTime.Now;

            _movieServiceMock.Setup(r => r.Create(movie)).ReturnsAsync(movie);

            //Act
            var actionResult = await _controller.Create(movie);

            //Assert
            var result = Assert.IsType<CreatedAtActionResult>(actionResult);
            var model = Assert.IsAssignableFrom<Movie>(result.Value);
            Assert.Equal(201, result.StatusCode);
            Assert.Equal(movie.Id, model.Id);
        }

        [Fact]
        public async Task UpdateMovie()
        {
            // Arrange
            var movieToUpdate = new Movie { Id = 3 };
            var updatedMovie = new Movie { Id = 3, Title = "Pelicula nueva", Genre = GenreType.Comedia, ReleaseDate = DateTime.Now };

            _movieServiceMock.Setup(x => x.GetById(movieToUpdate.Id)).ReturnsAsync(movieToUpdate);
            _movieServiceMock.Setup(x => x.Update(It.IsAny<Movie>())).ReturnsAsync(updatedMovie);

            // Act
            var result = await _controller.Update(movieToUpdate);

            // Assert
            Assert.IsAssignableFrom<AcceptedAtActionResult>(result);
            var actionResult = (AcceptedAtActionResult)result;
            Assert.Equal(nameof(MovieController.Update), actionResult.ActionName);
            Assert.Equal(202, actionResult.StatusCode);

        }

        [Fact]
        public async Task DeleteMovie()
        {
            //Arrange 
            Movie movie = new Movie();

            // Arrange
            var movieId = 1;
            var existingMovie = new Movie { Id = movieId };

            _movieServiceMock.Setup(x => x.GetById(movieId)).ReturnsAsync(existingMovie);

            // Act
            var result = await _controller.Delete(movieId);

            // Assert
            Assert.IsAssignableFrom<NoContentResult>(result);
        }


        private List<Movie> InitDummyList()
        {

            var movieList = new List<Movie>();
            movieList.Add(new Movie() { Id = 1, Genre = GenreType.Terror, Title = "Pelicula 1", ReleaseDate = DateTime.Now });
            movieList.Add(new Movie() { Id = 2, Genre = GenreType.Terror, Title = "Pelicula 2", ReleaseDate = DateTime.Now });
            movieList.Add(new Movie() { Id = 3, Genre = GenreType.Comedia, Title = "Pelicula 3", ReleaseDate = DateTime.Now });
            movieList.Add(new Movie() { Id = 4, Genre = GenreType.Others, Title = "Pelicula 4", ReleaseDate = DateTime.Now });
            movieList.Add(new Movie() { Id = 5, Genre = GenreType.Others, Title = "Pelicula 5", ReleaseDate = DateTime.Now });

            return movieList;
        }
    }
}
