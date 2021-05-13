using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ViewModelFun.Controllers
{
    public class User
    {
        public string FirstName {get;set;}
        public string LastName {get;set;}
    }

    public class ViewModelFunController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            string message = "This is a string passed in as a ViewModel!";
            return View("Index", message);
        }

        [HttpGet("numbers")]
        public IActionResult Numbers()
        {
            int[] nums = {1, 2, 3, 4, 5, 6, 7};
            return View(nums);
        }

        [HttpGet("users")]
        public IActionResult displayUsers()
        {
            List<User> Users = new List<User>()
            {
                new User(){
                    FirstName = "John",
                    LastName = "Patterson"
                },
                new User(){
                    FirstName = "Joe",
                    LastName = "Mann"
                },
                new User(){
                    FirstName = "Betty",
                    LastName = "Ross"
                },
                new User(){
                    FirstName = "Will",
                    LastName = "Roberts"
                }
            };

            return View(Users);
        }

        [HttpGet("user")]

        public IActionResult displayUser()
        {
            User singleUser = new User(){
                FirstName = "John",
                LastName = "Johnson"
            };

            return View(singleUser);
        }
    }
}