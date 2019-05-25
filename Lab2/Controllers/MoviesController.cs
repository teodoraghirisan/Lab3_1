using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab2.Models;
using Lab2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IMovieService movieService;
        public MoviesController(IMovieService expenseService)
        {
            this.movieService = expenseService;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET: api/Movies
        [HttpGet]
        public IEnumerable<Movie> Get([FromQuery]DateTime? from, [FromQuery]DateTime? to)
        {
            return movieService.GetAll(from, to);
            //IQueryable<Movie> result = context.Movies.Include(c => c.Comments).OrderByDescending(m => m.YearOfRelease);
            //if (from == null && to == null)
            //{
            //    return result;
            //}
            //if (from != null)
            //{
            //    result = result.Where(m => m.DateAdded >= from);
            //}
            //if (to != null)
            //{
            //    result = result.Where(m => m.DateAdded <= to);
            //}
            //return result;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET: api/Movies/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var found = movieService.GetById(id);
            if (found == null)
            {
                return NotFound();
            }

            return Ok(found);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // POST: api/Movies
        [HttpPost]
        
        public void Post([FromBody] Movie movie)
        {
            //if (!ModelState.IsValid)
            //{

            //}
            //context.Movies.Add(movie);
            //context.SaveChanges();
            movieService.Create(movie);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Movie movie)
        {
            //var existing = context.Movies.AsNoTracking().FirstOrDefault(m => m.Id == id);
            //if (existing == null)
            //{
            //    context.Movies.Add(movie);
            //    context.SaveChanges();
            //    return Ok(movie);
            //}
            //movie.Id = id;
            //context.Movies.Update(movie);
            //context.SaveChanges();
            //return Ok(movie);
            var result = movieService.Upsert(id, movie);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //var existing = context.Movies.FirstOrDefault(movie => movie.Id == id);
            //if (existing == null)
            //{
            //    return NotFound();
            //}
            //context.Movies.Remove(existing);
            //context.SaveChanges();
            //return Ok();
            var result = movieService.Delete(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
