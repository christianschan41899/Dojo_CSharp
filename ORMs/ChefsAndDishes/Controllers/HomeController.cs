using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChefModel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ChefsAndDishes.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            //Include ChefDishes so the # of dishes properly shows up
            ViewBag.Chefs = dbContext.Chefs
                .Include(chef => chef.ChefDishes)
                .OrderBy(ch => ch.ChefId);
            
            return View();
        }

        [HttpGet("new")]
        public IActionResult NewChefForm()
        {
            return View("ChefForm");
        }

        [HttpPost("new")]
        public IActionResult CreateChef(Chef newChef)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(newChef);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("ChefForm");
            }
        }

        [HttpGet("dishes")]
        public IActionResult Dishes()
        {
            //Include Creator (Otherwise null and throws error)
            ViewBag.Dishes = dbContext.Dishes
                .Include(dish => dish.Creator)
                .OrderBy(dhs => dhs.DishId);
            
            return View();
        }

        [HttpGet("dishes/new")]
        public IActionResult NewDish()
        {
            ViewBag.Chefs = dbContext.Chefs
                .OrderBy(ch => ch.ChefId);
            
            return View("DishForm");
        }

        [HttpPost("dishes/new")]
        public IActionResult CreateDish(Dish newDish)
        {
            if(ModelState.IsValid)
            {
                //Get the Chef from the Id newDish uses to reference its creators
                Chef CreatorChef = dbContext.Chefs.Include(chef => chef.ChefDishes).FirstOrDefault(c => c.ChefId == newDish.ChefId);
                //Accidentally left in Creator name as a field so handle here
                string ChefName = CreatorChef.FirstName + " " + CreatorChef.LastName;
                newDish.Chef = ChefName;
                //Assign newDish its creator.
                newDish.Creator = CreatorChef;
                //Add dish to 
                dbContext.Add(newDish);
                //Add newDish to the creator's dish list
                CreatorChef.ChefDishes.Add(newDish);
                //Save the changes
                dbContext.SaveChanges();
                return RedirectToAction("Dishes");
            }
            else
            {
                return View("DishForm");
            }
        }

    }
}
