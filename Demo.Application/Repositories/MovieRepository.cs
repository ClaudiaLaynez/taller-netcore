using Demo.Application.Interfaces;
using Demo.Domain;
using Demo.Infraestructure;
using Microsoft.EntityFrameworkCore;


namespace Demo.Application.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _context;

        public MovieRepository(MovieContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> GetAll()
        {
            return await _context.Movies.AsNoTracking().ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await _context.Movies.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Movie> Create(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;

        }

        public async Task Delete(Movie movie)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> Update(Movie movie)
        {
            _context.Movies.Update(movie);
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return movie;
        }
    }
}
