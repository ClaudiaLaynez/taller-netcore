using Demo.Application.Interfaces;
using Demo.Application.Services;
using Demo.Domain;
using Moq;

namespace Demo.Test
{
    public class MovieServiceTests
    {
        private readonly Mock<IMovieRepository> _mockRepository;
        private readonly MovieService _service;
        private List<Movie> movieList;

        public MovieServiceTests()
        {
            _mockRepository = new Mock<IMovieRepository>();
            _service = new MovieService(_mockRepository.Object);
            movieList = initDummyList();

        }

        [Fact]
        public async Task GetAll_Returns_All_MoviesAsync()
        {
            //Arrange
            _mockRepository.Setup(repository => repository.GetAll()).ReturnsAsync(movieList);


            // Act
            var result = await _service.GetAll();

            //Assert
            Assert.Equal(4, result.Count);
        }

        [Fact]
        public async Task Get_MovieById()
        {
            //Arrange
            int movieId = 1;
            _mockRepository.Setup(r => r.GetById(movieId)).ReturnsAsync(movieList.Where(x => x.Id == movieId).First);

            //Act
            var result = await _service.GetById(movieId);

            //Assert
            Assert.Equal(movieId, result.Id);
        }


        [Fact]
        public async Task Create_Movie()
        { 
            //Arrange
            Movie movie = new Movie(); 
            movie.Id = 5;
            movie.Genre = GenreType.Terror;
            movie.Title = "Pelicula 1";
            movie.ReleaseDate = DateTime.Now;

            _mockRepository.Setup(r => r.Create(movie)).ReturnsAsync(movie);

            //Act
            var result = await _service.Create(movie);

            //Assert
            Assert.Equal(movie.Id, result.Id);
        }

        [Fact]
        public async Task Update_Movie()
        {
            //Arrange
            Movie movie = new Movie();

            movie.Id = 2;
            movie.Genre = GenreType.Comedia;
            movie.Title = "Pelicula 22";
            movie.ReleaseDate = DateTime.Now;

            _mockRepository.Setup(r => r.Update(movie)).ReturnsAsync(movie);

            //Act
            var result = await _service.Update(movie);

            //Assert
            Assert.Equal(GenreType.Comedia, movie.Genre);

        }

        [Fact]
        public async Task Delete_Movie()
        {
            //Arrange 
            Movie movie = new Movie();

            movie.Id = 1;
            movie.Genre = GenreType.Terror;
            movie.Title = "Pelicula 1";
            movie.ReleaseDate = DateTime.Now;

            _mockRepository.Setup(r => r.Delete(movie));

            //Act
            await _service.Delete(movie);

            //Assert
            _mockRepository.Verify(repo => repo.Delete(movie), Times.Once);
        }


        private List<Movie> initDummyList()
        {

            var movieList = new List<Movie>();
            movieList.Add(new Movie() { Id = 1, Genre = GenreType.Terror, Title = "Pelicula 1", ReleaseDate = DateTime.Now });
            movieList.Add(new Movie() { Id = 2, Genre = GenreType.Terror, Title = "Pelicula 2", ReleaseDate = DateTime.Now });
            movieList.Add(new Movie() { Id = 3, Genre = GenreType.Comedia, Title = "Pelicula 3", ReleaseDate = DateTime.Now });
            movieList.Add(new Movie() { Id = 4, Genre = GenreType.Drama, Title = "Pelicula 4", ReleaseDate = DateTime.Now });

            return movieList;
        }
    }
}
