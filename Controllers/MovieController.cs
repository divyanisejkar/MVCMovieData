
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
        
       IMovieDataAccess _Imovie;

        public MovieController(IMovieDataAccess Imovie,IMapper mapper, IMediator mediator)
        {
            _Imovie = Imovie;
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
        public IActionResult Create(AddMovieRequest movie)
        {
           
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
         if(checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            var movie = _mediator.Send(new GetMovieDataRequest { Id = id });
            return View(_mapper.Map<MvcMovieContext>(movie.Result));
        }

        [HttpPost] 
        public IActionResult Edit( EditMovieRequest movie)
        {
                _mediator.Send(movie);
                return RedirectToAction("Index", "Movie");
         
        }

        [HttpGet]
        public IActionResult Detail(int? id)
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            var movie = _mediator.Send(new GetMovieDataRequest { Id = id });
            return View(_mapper.Map<MvcMovieContext>(movie.Result));
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            var movie = _mediator.Send(new GetMovieDataRequest { Id = id });

            return View(_mapper.Map<MvcMovieContext>(movie.Result));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            _mediator.Send(new DeleteMovieRequest { Id = id });
            return RedirectToAction("Index");
        }

        //[Route("logout")]
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