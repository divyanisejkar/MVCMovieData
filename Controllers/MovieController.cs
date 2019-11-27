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
        MovieDataAccess movieDataAccess = new MovieDataAccess();
        public IActionResult Index()
        {
            MovieDataAccess movieDataAccess = new MovieDataAccess();
            List<Movie> lstMovie = new List<Movie>();
            lstMovie = movieDataAccess.GetAllMovie().ToList();

            return View(lstMovie);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Movie movie)
        {
            MovieDataAccess movieDataAccess = new MovieDataAccess();
            if (ModelState.IsValid)
            {
                movieDataAccess.AddMovie(movie);
                return RedirectToAction("Index","Movie");
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
            Movie movie = movieDataAccess.GetMovieData(id);

            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind]Movie movie)
        {
            if (id != movie.ID)
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
            Movie movie = movieDataAccess.GetMovieData(id);

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
            Movie movie = movieDataAccess.GetMovieData(id);

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