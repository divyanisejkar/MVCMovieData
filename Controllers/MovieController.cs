using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieData.Models;

namespace MovieData.Controllers
{

    public class MovieController : Controller
    {
       // MovieDataAcessEF movieDataAccess;
        MovieDataAccess movieDataAccess = new MovieDataAccess();

        public MovieController(DbContextContext dbContext)
        {
            //movieDataAccess = new MovieDataAcessEF(dbContext);
        }
          

        public IActionResult Index()
        {
           // MovieDataAccess movieDataAccess = new MovieDataAccess();
            List<MvcMovieContext> lstMovie = new List<MvcMovieContext>();
            lstMovie = movieDataAccess.GetAllMovies();

            return View(lstMovie);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] MvcMovieContext movie)
        {
           // MovieDataAccess movieDataAccess = new MovieDataAccess();
            if (ModelState.IsValid)
            {

               
                movieDataAccess.AddMovie(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }
      
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MvcMovieContext movie = movieDataAccess.GetMovieData(id);

            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind]MvcMovieContext movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                movieDataAccess.UpdateMovie(movie);
                return RedirectToAction("Index", "Movie");
            }
            return View(movie);
        }

        [HttpGet]
        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MvcMovieContext movie = movieDataAccess.GetMovieData(id);

            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MvcMovieContext movie = movieDataAccess.GetMovieData(id);

            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            movieDataAccess.DeleteMovie(id);
            return RedirectToAction("Index");
        }

    }
}