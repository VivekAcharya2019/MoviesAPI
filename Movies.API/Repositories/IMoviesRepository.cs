using Movies.API.Context;
using Movies.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API.Repositories
{
    public interface IMoviesRepository
    {
        IEnumerable<Movie> GetAll();
        Movie Get(int id);
        void AddMovie(Movie movieToAdd);

        Task<bool> SaveChangesAsync();

        bool Save();
    }
}
