using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using loginAndRegistration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using loginAndRegistration.Factory;
using Microsoft.AspNetCore.Identity;

namespace loginAndRegistration.Controllers
{
    public class UserController : Controller
    {
        private readonly UserFactory userFactory;
        public UserController(UserFactory user)
        {
            userFactory = user;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("id") != null)
            {
                return RedirectToAction("show");
            }
            if(TempData["error"] == null)
            {
            ViewBag.Errors = "";
            ViewBag.User = "";
            }
            else{
                ViewBag.Errors = TempData["error"];
                ViewBag.User = "";
            }
            return View();
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Method(User newUser)
        {
            TryValidateModel(newUser);
            if(ModelState.IsValid)
            {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.password = Hasher.HashPassword(newUser, newUser.password);
                userFactory.Add(newUser);
                User user = userFactory.FindByEmail(newUser.email);
                HttpContext.Session.SetInt32("id", (int)user.Id);
                return RedirectToAction("show");
            }
            ViewBag.Errors = ModelState.Values;
            return View("index");
        }
        [HttpGet]
        [Route("show")]
        public IActionResult show()
        {
            if(HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("index");
            }
            ViewBag.User = userFactory.FindByID((int)HttpContext.Session.GetInt32("id"));
            return View();
        }
        [HttpPost]
        [Route("login")]
        public IActionResult login(string email, string password)
        {
            User user = userFactory.FindByEmail(email);
            if(user != null && password != null)
            {
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(user, user.password, password))
                {
                HttpContext.Session.SetInt32("id", (int)user.Id);
                return RedirectToAction("show");
                }
            }
                TempData["error"] = "Incorrect Email or Password";
                return View("index");
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
