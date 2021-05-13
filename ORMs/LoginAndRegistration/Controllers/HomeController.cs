using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LoginAndRegistration.Models;
using Microsoft.EntityFrameworkCore;
using UserModel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace LoginAndRegistration.Controllers
{
    public class HomeController : Controller
    {
        private MyContext DbContext;
     
        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            DbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost("")]
        public IActionResult CreateUser(User newUser)
        {
            if(ModelState.IsValid)
            {
                //Check if Email is already in use
                if(DbContext.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);

                DbContext.Add(newUser);
                DbContext.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View("LoginForm");
        }

        [HttpPost("login")]

        public IActionResult LoginHandler(LoginUser submitUser)
        {
            if(ModelState.IsValid)
            {
                User userInDb = DbContext.Users.FirstOrDefault(u => u.Email == submitUser.Email);
                // If no user exists with provided email
                if(userInDb == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("LoginForm");
                }
                
                var hasher = new PasswordHasher<LoginUser>();
                // verify provided password against hash stored in db
                var result = hasher.VerifyHashedPassword(submitUser, userInDb.Password, submitUser.Password);
                
                // result can be compared to 0 for failure
                if(result == 0)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("LoginForm");
                }
                
                HttpContext.Session.SetObjectAsJson("LoggedUser", userInDb);
                return RedirectToAction("Success");
            }
            else
            {
                return View("LoginForm");
            }
        }

        [HttpGet("success")]
        public IActionResult Success()
        {
            User RetrieveUser = HttpContext.Session.GetObjectFromJson<User>("LoggedUser");
            if(RetrieveUser == null)
            {
                ModelState.AddModelError("Email", "No user logged in! Log in or Register!");
                return View("LoginForm");
            }
            else
            {
                return View("Success");
            }
        }
    }

    public static class SessionExtensions
    {
        // We can call ".SetObjectAsJson" just like our other session set methods, by passing a key and a value
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            // This helper function simply serializes the object to JSON and stores it as a string in session
            session.SetString(key, JsonSerializer.Serialize(value));
        }
        
        // generic type T is a stand-in indicating that we need to specify the type on retrieval
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            // Upon retrieval the object is deserialized based on the type we specified
            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        }
    }
}
