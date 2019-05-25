using Lab2.DTOs;
using Lab2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Services
{
    public interface ICommentService
    {

        IEnumerable<CommentsGetDTO> GetAll(String filter);

    }

    public class CommentService : ICommentService
    {

        private MoviesDbContext context;

        public CommentService(MoviesDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<CommentsGetDTO> GetAll(String filter)
        {
            IQueryable<Movie> result = context.Movies.Include(c => c.Comments);

            List<CommentsGetDTO> resultComments = new List<CommentsGetDTO>();
            List<CommentsGetDTO> resultCommentsAll = new List<CommentsGetDTO>();

            foreach (Movie movie in result)
            {
                movie.Comments.ForEach(c =>
                {
                    if (c.Text == null || filter == null)
                    {
                        CommentsGetDTO comment = new CommentsGetDTO
                        {
                            Id = c.Id,
                            Important = c.Important,
                            Text = c.Text,
                            MovieId = movie.Id

                        };
                        resultCommentsAll.Add(comment);
                    }
                    else if (c.Text.Contains(filter))
                    {
                        CommentsGetDTO comment = new CommentsGetDTO
                        {
                            Id = c.Id,
                            Important = c.Important,
                            Text = c.Text,
                            MovieId = movie.Id

                        };
                        resultComments.Add(comment);

                    }
                });
            }
            if (filter == null)
            {
                return resultCommentsAll;
            }
            return resultComments;
        }
    }
}

