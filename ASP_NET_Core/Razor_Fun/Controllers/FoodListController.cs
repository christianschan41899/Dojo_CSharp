using Microsoft.AspNetCore.Mvc;
namespace FoodList.Controllers
{
    public class FoodListController : Controller
    {
        [HttpGet("")]

        public ViewResult Index()
        {
            return View("FoodList");
        }
    }
}