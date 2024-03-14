using Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Interfaces
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAll();
        Task<Movie> GetById(int id);
        Task<Movie> Create(Movie movie);
        Task<Movie> Update(Movie movie);
        Task Delete(Movie movie);
    }
}
