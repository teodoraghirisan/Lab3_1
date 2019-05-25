using Lab2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Services
{
    public interface IMovieService
    {
        IEnumerable<Movie> GetAll(DateTime? from = null, DateTime? to = null);
        Movie GetById(int id);
        Movie Create(Movie movie);
        Movie Upsert(int id, Movie movie);
        Movie Delete(int id);
    }

    public class MovieService : IMovieService
    {
        //aici tb sa declar un DB context
        private MoviesDbContext context;
        //tb constructor
        public MovieService(MoviesDbContext context)
        {
            this.context = context;
        }

        //acum mutam logica din Controller pe Service. 
        //Nu il eliminam dar Controller-ul va apela Service si nu va mai apela UI-ul Service-ul

        public Movie Create(Movie movie)
        {
            context.Movies.Add(movie);
            context.SaveChanges();
            return movie;
        }

        public Movie Delete(int id)
        {
            var existing = context.Movies.Include(x => x.Comments).FirstOrDefault(movie => movie.Id == id);
            //var existing = context.Movies.FirstOrDefault(movie => movie.Id == id);
            if (existing == null)
            {
                return null;
            }
            context.Movies.Remove(existing);
            context.SaveChanges();

            return existing;
        }
        public IEnumerable<Movie> GetAll(DateTime? from = null, DateTime? to = null)
        {
            //IQueryable<Movie> result = context.Movies.Include(f => f.Comments);
            IQueryable<Movie> result = context.Movies.Include(c => c.Comments).OrderByDescending(m => m.YearOfRelease);
            if (from == null && to == null)
            {
                return result;
            }
            if (from != null)
            {
                result = result.Where(f => f.DateAdded >= from);
            }
            if (to != null)
            {
                result = result.Where(f => f.DateAdded <= to);
            }
            return result;
        }

        public Movie GetById(int id)
        {
            // sau context.Expenses.Find()
            return context.Movies
                .Include(f => f.Comments)
                .FirstOrDefault(f => f.Id == id);
        }


        public Movie Upsert(int id, Movie movie)
        {
            var existing = context.Movies.AsNoTracking().FirstOrDefault(f => f.Id == id);
            if (existing == null)
            {
                context.Movies.Add(movie);
                context.SaveChanges();
                return movie;
            }
            movie.Id = id;
            context.Movies.Update(movie);
            context.SaveChanges();
            return movie;
        }


    }

}


