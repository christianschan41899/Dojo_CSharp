using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using UserModel.Models;
using ActModel.Models;
using ActingUserModel.Models;
using ActivitiesUsersContext.Models;

namespace Exam1.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        /**************************
            Login and Registration
        ****************************/
        [HttpGet("signin")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult CreateUser(User newUser)
        {
            if(ModelState.IsValid)
            {
                //Check if Email is already in use
                if(dbContext.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("Index");
                }

                //Hash password
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                
                //Store in db and assign to Session
                dbContext.Add(newUser);
                dbContext.SaveChanges();
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
                User userInDb = dbContext.Users.FirstOrDefault(u => u.Email == submitUser.LoginEmail);
                // If no user exists with provided email
                if(userInDb == null)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
                    return View("Index");
                }
                
                var hasher = new PasswordHasher<LoginUser>();
                // verify provided password against hash stored in db
                var result = hasher.VerifyHashedPassword(submitUser, userInDb.Password, submitUser.LoginPassword);
                
                // result can be compared to 0 for failure
                if(result == 0)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
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

        /************************************
                Activity Dashboard
        *************************************/
        [HttpGet("home")]
        public IActionResult Dashboard()
        {
            User RetrieveUser = HttpContext.Session.GetObjectFromJson<User>("LoggedUser");
            if(RetrieveUser == null)
            {
                ModelState.AddModelError("Name", "No user logged in! Log in or Register!");
                return View("Index");
            }
            else
            {
                ViewBag.uid = RetrieveUser.UserID;
                ViewBag.name = RetrieveUser.Name;
                List<Act> ActList = dbContext.Activities
                    .Include(act => act.Attendees)
                    .Include(act => act.Creator)
                    .OrderBy(act => act.Date).ToList();
                return View("ActivitiesList", ActList);
            }
        }

        /************************************
                    Activity Form
        *************************************/
        [HttpGet("new")]
        public IActionResult NewAct()
        {
            User RetrieveUser = HttpContext.Session.GetObjectFromJson<User>("LoggedUser");
            if(RetrieveUser == null)
            {
                ModelState.AddModelError("Name", "No user logged in! Log in or Register!");
                return View("Index");
            }
            return View("ActivityForm");
        }
        /*
            Add activity
        */
        [HttpPost("new")]
        public IActionResult CreateAct(Act newAct)
        {
            User RetrieveUser = HttpContext.Session.GetObjectFromJson<User>("LoggedUser");
            if(ModelState.IsValid)
            {
                User loggedUser = dbContext.Users
                    .FirstOrDefault(user => user.UserID == RetrieveUser.UserID); 
                //Add logged in user as creator
                newAct.UserID = loggedUser.UserID;
                //Add Activity
                dbContext.Add(newAct);
                //Save changes
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View("ActivityForm");
            }
        }

        /*******************************
                Display Activity
        ********************************/
        [HttpGet("activities/{activityid}")]
        public IActionResult GetActivity(int activityid)
        {
            User RetrieveUser = HttpContext.Session.GetObjectFromJson<User>("LoggedUser");
            if(RetrieveUser == null)
            {
                ModelState.AddModelError("Name", "No user logged in! Log in or Register!");
                return View("Index");
            }
            ViewBag.uid = RetrieveUser.UserID;
            Act currentActivity = dbContext.Activities
                .Include(act => act.Creator)
                .Include(act => act.Attendees)
                    .ThenInclude(attendees => attendees.Attendee)
                .FirstOrDefault(act => act.ActID == activityid);
            
            return View("ActivityDisplay", currentActivity);
        }

        /************************************
                    Action Methods
        *************************************/

        //Join an activity
        [HttpGet("activities/{activityid}/join")]
        public IActionResult JoinActivity(int activityid)
        {
            User RetrieveUser = HttpContext.Session.GetObjectFromJson<User>("LoggedUser");
            ActingUser newRelationship = new ActingUser();
            newRelationship.ActID = activityid;
            newRelationship.UserID = RetrieveUser.UserID;
            dbContext.Add(newRelationship);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        //Leave an activity
        [HttpGet("activities/{activityid}/leave")]
        public IActionResult LeaveActivity(int activityid)
        {
            User RetrieveUser = HttpContext.Session.GetObjectFromJson<User>("LoggedUser");
            ActingUser removeRelationship = dbContext.ActingUsers
                .FirstOrDefault(rel => (rel.ActID == activityid && rel.UserID == RetrieveUser.UserID));
            
            dbContext.ActingUsers.Remove(removeRelationship);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        //Delete an activity
        [HttpGet("activities/{activityid}/delete")]
        public IActionResult DeleteActivity(int activityid)
        {
            Act removeActivity = dbContext.Activities
                .FirstOrDefault(act => act.ActID == activityid );
            
            dbContext.Activities.Remove(removeActivity);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    }


    /********************************************************
        Allows Session to store and retireve objects as 
        JSON strings
    ********************************************************/
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }
        
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        }
    }
}
