using Microsoft.AspNetCore.Mvc;

namespace TimeDisplay.Controllers
{
    public class TimeDisplayController : Controller
    {
        [HttpGet("")]
        
        public ViewResult Clock()
        {
            return View("TimeDisplay");
        }
    }
}