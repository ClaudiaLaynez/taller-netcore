using Demo.Domain;

namespace Demo.Application.Interfaces
{
    public interface IMovieService
    {
        Task<List<Movie>> GetAll();
        Task<Movie> GetById(int id);
        Task<Movie> Create(Movie customer);
        Task<Movie> Update(Movie customer);
        Task Delete(Movie customer);
    }
}
