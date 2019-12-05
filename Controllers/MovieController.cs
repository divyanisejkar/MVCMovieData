
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
        private readonly IMapper _mapper;
        
       IMovieDataAccess Imovie;

        public MovieController(DbContextContext context,IMapper mapper, IMediator mediator)
        {
            Imovie = new MovieDataAccess();
            _mediator = mediator;
            _mapper = mapper;
        }
          

        public IActionResult Index()
        {
            if(checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }

            

            return View(_mediator.Send(new GetAllMoviesRequest()).Result.Success);


            //List<MvcMovieContext> lstMovie = new List<MvcMovieContext>();
            //lstMovie = Imovie.GetAllMovies();

            //return View(lstMovie);

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
        public IActionResult Create(AddMovieRequest movie)
        {
           
          /*  if (checkInvalidSession())
            {
                return RedirectToAction("Home","Index");
            }
             Imovie.AddMovie(movie);
             return RedirectToAction(nameof(Index));
            // return View(movie);*/
            if (ModelState.IsValid)
            {
                _mediator.Send(movie);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(movie);
            }
        }
      
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            /*  if (id == null)
              {
                  return NotFound();
              }
              MvcMovieContext movie = Imovie.GetMovieData(id);

              if (movie == null)
              {
                  return NotFound();
              }
              return View(movie);*/
            var movie = _mediator.Send(new GetMovieDataRequest { Id = id });
            return View(_mapper.Map<MvcMovieContext>(movie.Result));
        }

        [HttpPost]
        
        public IActionResult Edit( EditMovieRequest movie)
        {
            
                _mediator.Send(movie);
                //Imovie.UpdateMovie(movie);
                return RedirectToAction("Index", "Movie");
         
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

      
       /* public IActionResult Delete(int? id)
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
        }*/

          public IActionResult Delete(int? id)
        {
            var movie = _mediator.Send(new GetMovieDataRequest { Id = id });

            return View(_mapper.Map<MvcMovieContext>(movie.Result));
        }


        [HttpPost, ActionName("Delete")]
        
        public IActionResult DeleteConfirmed(int? id)
        {
            // Imovie.DeleteMovie(id);
            _mediator.Send(new DeleteMovieRequest { Id = id });
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