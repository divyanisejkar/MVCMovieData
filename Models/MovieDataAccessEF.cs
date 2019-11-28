using Microsoft.EntityFrameworkCore;
using MovieData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieData.Models
{
    public class MovieDataAcessEF
    {
        DbContextContext movieAppDBContext;

        public MovieDataAcessEF(DbContextContext context)
        {
            movieAppDBContext = context;
        }

        public void AddMovie(MvcMovieContext movie)
        {
            movieAppDBContext.Add(movie);
            movieAppDBContext.SaveChanges();
        }

        public List<MvcMovieContext> GetAllMovies()
        {
            return movieAppDBContext.MvcMovieContext.ToList(); ;
        }

        public void DeleteMovie(int? id)
        {
            var movie = movieAppDBContext.MvcMovieContext.Find(id);
            movieAppDBContext.MvcMovieContext.Remove(movie);
            movieAppDBContext.SaveChanges();
        }

        public void UpdateMovie(MvcMovieContext movie)
        {
            movieAppDBContext.Update(movie);
            movieAppDBContext.SaveChanges();
        }

        public MvcMovieContext GetMovieData(int? id)
        {
            MvcMovieContext movie = movieAppDBContext.MvcMovieContext.Find(id);
            return movie;
        }
    }
}
