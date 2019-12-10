using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieData.Models
{
   public interface IMovieDataAccess
    {
        public List<MvcMovieContext> GetAllMovies();

        public bool AddMovie(MvcMovieContext movie);
        public void UpdateMovie(MvcMovieContext movie);

        public MvcMovieContext GetMovieData(int? id);

        public void DeleteMovie(int? id);
    }
}
