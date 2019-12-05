using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieData.Models;
using static MovieData.Models.LoginHandler;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace MovieData.Controllers
{
    public class HomeController : Controller
    {
        UserDataAccess userDataAccess = new UserDataAccess();
        private readonly ILogger<HomeController> _logger;

        private readonly IMediator _mediator;

        private readonly IMapper _mapper;
        //Constructor Injection
        public HomeController(ILogger<HomeController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
       
        public IActionResult Login(RequestModel user)
        {
            //UserDataAccess userDataAccess = new UserDataAccess();

            //string EmailID = user.EmailID;
            // string Password = user.Password;

            //bool success = userDataAccess.CheckUserLogin(EmailID, Password);
            var result = _mediator.Send(new RequestModel() { EmailID = user.EmailID, Password = user.Password });

            bool success = result.Result.Success;

            if (success == true)
            {
                HttpContext.Session.SetString("email", user.EmailID);
                return RedirectToAction("Index", "Movie");
            }
            else
            {
                ViewBag.error = "Invalid Account";
                return View("Login");
            }

        }


        public IActionResult Register()
        {
            return View();
        }


        // public ActionResult Register(User user)
        //{

        // UserDataAccess userDataAccess = new UserDataAccess();
        // userDataAccess.addUser(user);
        // return View("Login");

        // }

        [HttpPost]
        public ActionResult Register(RegisterRequestModel registerRequestModel)
        {

             //bool success = userDataAccess.addUser(user);
            var result = _mediator.Send(registerRequestModel);

           // bool success = result.Result.Success;
            if (result.Result.Success)
            {
                ViewData["EmailID"] = registerRequestModel.EmailID;
                return View("Login");
            }
            else
            {
                ViewData["Error"] = "Registraion Failed User Already Exists";
                return View();
            }
        }


       // public JsonResult doesUserNameExist(string EmailID)
        //{

         // var user = userDataAccess.CheckUserDetails(EmailID);

         // return Json(user == null);
         //}


        public IActionResult Forgot()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Forgot([Bind] User user)
        {
            UserDataAccess userDataAccess = new UserDataAccess();

            string EmailID = user.EmailID;
            string old_pwd = user.old_pwd;
            string new_pwd = user.new_pwd;


            bool success= userDataAccess.CheckPassword(EmailID,old_pwd,new_pwd);
            if (success==true)
            {

                return RedirectToAction("Login");
            }
            else
            {
                return View("Forgot");
            }


        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword([Bind] ForgotRequestModel user)
        {
            // UserDataAccess userDataAccess = new UserDataAccess();

            // string EmailID = user.EmailID;
            // string new_pwd = user.new_pwd;


            //bool success = userDataAccess.NewPassword(EmailID, new_pwd);
           
            var result = _mediator.Send(new ForgotRequestModel() { EmailID = user.EmailID, new_pwd = user.new_pwd });

            bool success = result.Result.Success;
            if (success == true)
            {

                return RedirectToAction("Login");
            }
            else
            {
                return View("ForgotPassword");
            }


        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       


    }
}
