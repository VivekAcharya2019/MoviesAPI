using Microsoft.EntityFrameworkCore;
using Movies.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API.Context
{
    public class MoviesDBcontext : DbContext
    {
        public MoviesDBcontext(DbContextOptions<MoviesDBcontext> o):base(o)
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }


    }
}
