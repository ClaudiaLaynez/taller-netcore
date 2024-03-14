using Demo.Application.Interfaces;
using Demo.Domain;
using Demo.Infraestructure;
using Microsoft.EntityFrameworkCore;

namespace Demo.Application.Services
{
    public class MovieService : IMovieService
    {

        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<List<Movie>> GetAll()
        {
            return await _movieRepository.GetAll();
        }

        public async Task<Movie> GetById(int id)
        {
            return await _movieRepository.GetById(id);
        }

        public async Task<Movie> Create(Movie movie)
        {
            return await _movieRepository.Create(movie);
        }

        public async Task Delete(Movie movie)
        {
            await _movieRepository.Delete(movie);
        }

        public async Task<Movie> Update(Movie movie)
        {
            return await _movieRepository.Update(movie);
        }
    }
}
