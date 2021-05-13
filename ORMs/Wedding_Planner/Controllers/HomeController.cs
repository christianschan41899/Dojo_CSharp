using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserModel.Models;
using WeddingModel.Models;
using AttendWeddingModel.Models;
using Microsoft.EntityFrameworkCore;
using WeddingPlannerContext.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace Wedding_Planner.Controllers
{
    public class HomeController : Controller
    {
        private MyContext DbContext;
        public HomeController(MyContext context)
        {
            DbContext = context;
        }
        
        /*Login and Registration*/
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("")]
        public string Message()
        {
            return "Form sending request to wrong action!";
        }

        [HttpPost("register")]
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
                HttpContext.Session.SetObjectAsJson("LoggedUser", newUser);
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View("Index");
            }
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
                    return View("Index");
                }
                
                var hasher = new PasswordHasher<LoginUser>();
                // verify provided password against hash stored in db
                var result = hasher.VerifyHashedPassword(submitUser, userInDb.Password, submitUser.Password);
                
                // result can be compared to 0 for failure
                if(result == 0)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Index");
                }
                
                HttpContext.Session.SetObjectAsJson("LoggedUser", userInDb);
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        /*Wedding Dashboard*/
        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            User RetrieveUser = HttpContext.Session.GetObjectFromJson<User>("LoggedUser");
            if(RetrieveUser == null)
            {
                ModelState.AddModelError("FirstName", "No user logged in! Log in or Register!");
                return View("Index");
            }
            else
            {
                ViewBag.uid = RetrieveUser.UserID;
                List<Wedding> WeddingList = DbContext.Weddings
                    .Include(wedding => wedding.Attendees)
                    .OrderBy(wedding => wedding.WeddingID).ToList();
                return View("WeddingDashboard", WeddingList);
            }
        }

        [HttpGet("wedding/new")]

        public IActionResult NewWedding()
        {
            User RetrieveUser = HttpContext.Session.GetObjectFromJson<User>("LoggedUser");
            if(RetrieveUser == null)
            {
                ModelState.AddModelError("FirstName", "No user logged in! Log in or Register!");
                return View("Index");
            }
            else
            {
                return View("WeddingForm");
            }
        }

        [HttpPost("wedding/new")]

        public IActionResult CreateWedding(Wedding newWedding)
        {
            User RetrieveUser = HttpContext.Session.GetObjectFromJson<User>("LoggedUser");
            if(ModelState.IsValid)
            {
                User loggedUser = DbContext.Users
                    .FirstOrDefault(user => user.UserID == RetrieveUser.UserID); 
                //Add logged in user as creator
                newWedding.UserID = loggedUser.UserID;
                //Logged in user will attend, so create many to many with the Session user and new wedding
                DbContext.Add(newWedding);
                //Save changes
                DbContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View("WeddingForm");
            }
        }

        [HttpPost("wedding/{weddingid}/delete")]

        public IActionResult DeleteWedding(int weddingid)
        {
            Wedding deleteWedding = DbContext.Weddings
                .FirstOrDefault(w => w.WeddingID == weddingid);
            
            DbContext.Weddings.Remove(deleteWedding);
            DbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpPost("wedding/{weddingid}/rsvp")]
        public IActionResult RSVPWedding(int weddingid)
        {
            User RetrieveUser = HttpContext.Session.GetObjectFromJson<User>("LoggedUser");
            AttendWedding newAttendee = new AttendWedding();
            newAttendee.UserID = RetrieveUser.UserID;
            newAttendee.WeddingID = weddingid;
            DbContext.Add(newAttendee);
            DbContext.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpPost("wedding/{weddingid}/unrsvp")]
        public IActionResult UnRSVPWedding(int weddingid)
        {
            User RetrieveUser = HttpContext.Session.GetObjectFromJson<User>("LoggedUser");
            AttendWedding deleteAttendee =  DbContext.AttendWeddings
                .FirstOrDefault(aw => (aw.WeddingID == weddingid && aw.UserID == RetrieveUser.UserID));
            
            DbContext.AttendWeddings.Remove(deleteAttendee);
            DbContext.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpGet("weddings/{weddingid}")]
        public IActionResult WeddingDetails(int weddingid)
        {
            Wedding currentWedding = DbContext.Weddings
                .Include(w => w.Attendees)
                    .ThenInclude(a => a.Attendee)
                .FirstOrDefault(w => w.WeddingID == weddingid);
            
            return View("WeddingDisplay", currentWedding);
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
