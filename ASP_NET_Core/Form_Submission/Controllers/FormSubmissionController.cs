using Microsoft.AspNetCore.Mvc;
using UserModel.Models;

namespace FormSubmission.Controllers
{
    public class FormSubmissionController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View("FormPage");
        }

        [HttpPost("user/create")]
        public IActionResult Create(User IncommingUser)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction("Success");
            }
            else
            {
                return View("FormPage");
            }
        }

        [HttpGet("user/create")]
        public IActionResult Success()
        {
            return View("Success");
        }
    }
}