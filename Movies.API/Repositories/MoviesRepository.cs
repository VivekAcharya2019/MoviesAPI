using Microsoft.EntityFrameworkCore;
using Movies.API.Context;
using Movies.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly MoviesDBcontext moviesDBcontext;

        public MoviesRepository(MoviesDBcontext moviesDBcontext)
        {
            this.moviesDBcontext = moviesDBcontext;
        }

        public IEnumerable<Movie> GetAll()
        {
            return moviesDBcontext.Movies.Include(m => m.Director).ToList();
        }

        public Movie Get(int id)
        {
            return moviesDBcontext.Movies.Include(m=>m.Director).Where(m=>m.ID==id).FirstOrDefault();
        }

        public void AddMovie(Movie movieToAdd)
        {
            if (movieToAdd == null)
            {
                throw new ArgumentNullException(nameof(movieToAdd));
            }

            moviesDBcontext.Add(movieToAdd);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await moviesDBcontext.SaveChangesAsync() > 0);
        }

        public bool Save()
        {
            return (moviesDBcontext.SaveChanges() > 0);
        }
    }
}
