using Microsoft.AspNetCore.Mvc;
using UserModel.Models;

namespace DojoSurvey.Controllers
{
    public class DojoSurvey : Controller
    {
        [HttpGet("")]

        public ViewResult FormPage()
        {
            return View("FormPage");
        }

        [HttpPost("display")]

        public IActionResult FormDisplay(User formUser)
        {
            if(ModelState.IsValid)
            {
                return View("FormDisplay", formUser);
            }
            else
            {
                return View("FormPage");
            }
        }
    }
}