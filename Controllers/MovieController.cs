using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieData.Models;
using MediatR;
using AutoMapper;


namespace MovieData.Controllers
{

    public class MovieController : Controller
    {
        // MovieDataAcessEF movieDataAccess;
        // MovieDataAccess movieDataAccess = new MovieDataAccess();

       private readonly IMediator _mediator;
        
        IMovieDataAccess Imovie;

        public MovieController(DbContextContext dbContext, IMediator mediator)
        {
            Imovie = new MovieDataAccess();
            _mediator = mediator;
        }
          

        public IActionResult Index()
        {
            if(checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }

            // var result = _mediator.Send(new GetAllMoviesRequest());

            // List<MvcMovieContext> sucess= result.Result.Success;

            return View(_mediator.Send(new GetAllMoviesRequest()).Result.Success);


          /*  List<MvcMovieContext> lstMovie = new List<MvcMovieContext>();
            lstMovie = Imovie.GetAllMovies();

            return View(lstMovie);*/

        }
        [HttpGet]
        public IActionResult Create()
        {
            if (checkInvalidSession())
            {
                return RedirectToAction( "Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] MvcMovieContext movie)
        {
           // MovieDataAccess movieDataAccess = new MovieDataAccess();
            if (checkInvalidSession())
            {
                return RedirectToAction("Home","Index");
            }
            Imovie.AddMovie(movie);
            return RedirectToAction(nameof(Index));
           // return View(movie);
        }
      
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MvcMovieContext movie = Imovie.GetMovieData(id);

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
                Imovie.UpdateMovie(movie);
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
            MvcMovieContext movie = Imovie.GetMovieData(id);

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
            MvcMovieContext movie = Imovie.GetMovieData(id);

            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        
        public IActionResult DeleteConfirmed(int? id)
        {
            Imovie.DeleteMovie(id);
            return RedirectToAction("Index");
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("email");
            return RedirectToAction("Index","Home");
        }

        public bool checkInvalidSession()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                TempData["SessionError"] = "Invalid Session. Login Again";
                return true;
            }
            else
                return false;
        }

    }
}