using Microsoft.AspNetCore.Mvc;
namespace Portfolio.Controllers
{
    public class PortfolioController : Controller
    {
        [HttpGet("")]

        public ViewResult Index()
        {
            return View("About");
        }

        [HttpGet("contact")]

        public ViewResult Contact()
        {
            return View("Contact");
        }

        [HttpGet("projects")]

        public ViewResult Projects()
        {
            return View("Projects");
        }
    }
}