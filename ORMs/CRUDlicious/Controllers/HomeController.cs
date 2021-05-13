using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DishModel.Models;

namespace CRUDlicious.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;
     
        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index(){
            ViewBag.Dishes = _context.Dishes
                .OrderByDescending(d => d.CreatedAt);
            return View();
        }

        [HttpGet("new")]
        public IActionResult NewDish(){
            return View("NewDishForm");
        }

        [HttpPost("new")]
        public IActionResult CreateDish(Dish newDish)
        {
            if(ModelState.IsValid)
            {
                _context.Add(newDish);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("NewDishForm");
            }
        }

        [HttpGet("{id}")]
        public IActionResult DishDetails(int id)
        {
            Dish getDish = _context.Dishes
                .SingleOrDefault(dish => dish.DishId == id);
            
            return View("DishInfo", getDish);
        }

        [HttpGet("/edit/{id}")]
        public IActionResult EditDish(int id)
        {
            Dish editingDish = _context.Dishes
                .SingleOrDefault(dish => dish.DishId == id);
            
            return View("EditDishForm", editingDish);
        }

        [HttpPost("/edit/{id}")]
        public IActionResult UpdateDish(int id, Dish updateData)
        {
            if(ModelState.IsValid)
            {
                Dish updatingDish = _context.Dishes
                    .SingleOrDefault(dish => dish.DishId == id);

                updatingDish.Name = updateData.Name;
                updatingDish.Chef = updateData.Chef;
                updatingDish.Calories = updateData.Calories;
                updatingDish.Tastiness = updateData.Tastiness;
                updatingDish.Description = updateData.Description;
                updatingDish.UpdatedAt = DateTime.Now;
                _context.SaveChanges();

                return Redirect($"../{id}");
            }
            else
            {
                return View("EditDishForm", updateData);
            }
        }

        [HttpPost("/delete/{id}")]
        public IActionResult DeleteDish(int id)
        {
            Dish deleteDish = _context.Dishes
                    .SingleOrDefault(dish => dish.DishId == id);
            
            _context.Dishes.Remove(deleteDish);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
