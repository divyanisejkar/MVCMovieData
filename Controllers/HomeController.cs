using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieData.Models;

namespace MovieData.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
        public IActionResult Login([Bind] User user)
        {
            UserDataAccess userDataAccess = new UserDataAccess();

            string EmailID = user.EmailID;
            string Password = user.Password;

            bool success = userDataAccess.CheckUserLogin(EmailID, Password);
            if (success)
            {

                return RedirectToAction("Index","Movie");
            }
            else
            {
                return View("Login");
            }
                           
        }


        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind] User user)
        {
            if (ModelState.IsValid)
            {
                UserDataAccess userDataAccess = new UserDataAccess();
                userDataAccess.addUser(user);
                return RedirectToAction("Login");
            }
            return View(user);
        }

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
        public IActionResult ForgotPassword([Bind] User user)
        {
            UserDataAccess userDataAccess = new UserDataAccess();

            string EmailID = user.EmailID;
            string new_pwd = user.new_pwd;


            bool success = userDataAccess.NewPassword(EmailID, new_pwd);
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
