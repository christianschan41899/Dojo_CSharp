using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dojodachi.Models;
using Microsoft.AspNetCore.Http;

namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]

        public IActionResult Initialize() //Initialize session variables
        {
            HttpContext.Session.SetInt32("Fullness", 20);
            HttpContext.Session.SetInt32("Happiness", 20);
            HttpContext.Session.SetInt32("Energy", 50);
            HttpContext.Session.SetInt32("Meals", 3);
            HttpContext.Session.SetString("Message", "Game Start!");
            return RedirectToAction("Dojodachi");
        }

        [HttpGet("dojodachi")]

        public IActionResult Dojodachi()
        {
            if(HttpContext.Session.GetInt32("Fullness").GetValueOrDefault() <= 0 || HttpContext.Session.GetInt32("Happiness").GetValueOrDefault() <= 0 )
            {
                HttpContext.Session.SetString("Message", "Your Dojodachi died! Game Over!");
            }
            else if(HttpContext.Session.GetInt32("Fullness").GetValueOrDefault() >= 100 && HttpContext.Session.GetInt32("Happiness").GetValueOrDefault() >= 100 && HttpContext.Session.GetInt32("Energy").GetValueOrDefault() >= 100)
            {
                HttpContext.Session.SetString("Message", "You Win!");
            }
            ViewBag.Fullness = HttpContext.Session.GetInt32("Fullness");
            ViewBag.Happiness = HttpContext.Session.GetInt32("Happiness");
            ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
            ViewBag.Meals = HttpContext.Session.GetInt32("Meals");
            ViewBag.Message = HttpContext.Session.GetString("Message");
            return View();
        }

        //Feed actions +5-10 Fullness, -1 Meals
        [HttpPost("feed")]
        public IActionResult Feed()
        {
            Random food = new Random();
            int gain = food.Next(5, 11);
            int displeasure = food.Next(4);
            if( HttpContext.Session.GetInt32("Meals").GetValueOrDefault() <= 0)
            {
                HttpContext.Session.SetString("Message", "You have no meals to give! Work to get some!");
            }
            else if(displeasure == 0)
            {
                HttpContext.Session.SetInt32("Meals",  HttpContext.Session.GetInt32("Meals").GetValueOrDefault() - 1);
                HttpContext.Session.SetString("Message", "Your Dojodachi refused to eat the food!");
            }
            else
            {
                HttpContext.Session.SetInt32("Fullness",  HttpContext.Session.GetInt32("Fullness").GetValueOrDefault() + gain);
                HttpContext.Session.SetInt32("Meals",  HttpContext.Session.GetInt32("Meals").GetValueOrDefault() - 1);
                HttpContext.Session.SetString("Message", $"Ate and gained {gain} Fullness!");
            }

            return RedirectToAction("Dojodachi");
        }

        //Play actions +5-10 Happiness, 
        [HttpPost("play")]
        public IActionResult Play()
        {
            Random play = new Random();
            int gain = play.Next(5, 11);
            int displeasure = play.Next(4);
            if( HttpContext.Session.GetInt32("Energy").GetValueOrDefault() < 5)
            {
                HttpContext.Session.SetString("Message", "You have no energy! Sleep to regain it!");
            }
            else if(displeasure == 0)
            {
                HttpContext.Session.SetInt32("Energy",  HttpContext.Session.GetInt32("Energy").GetValueOrDefault() - 5);
                HttpContext.Session.SetString("Message", "Your Dojodachi didn't seem too happy after playing...");
            }
            else
            {
                HttpContext.Session.SetInt32("Happiness",  HttpContext.Session.GetInt32("Happiness").GetValueOrDefault() + gain);
                HttpContext.Session.SetInt32("Energy",  HttpContext.Session.GetInt32("Energy").GetValueOrDefault() - 5);
                HttpContext.Session.SetString("Message", $"Played with your pet and gained {gain} Happiness!");
            }

            return RedirectToAction("Dojodachi");
        }

        //Work actions
        [HttpPost("work")]
        public IActionResult Work()
        {
            Random work = new Random();
            if( HttpContext.Session.GetInt32("Energy").GetValueOrDefault() < 5)
            {
                HttpContext.Session.SetString("Message", "You have no energy! Sleep to regain it!");
            }
            else
            {
                int gain = work.Next(1, 4);
                HttpContext.Session.SetInt32("Meals",  HttpContext.Session.GetInt32("Meals").GetValueOrDefault() + gain);
                HttpContext.Session.SetInt32("Energy",  HttpContext.Session.GetInt32("Energy").GetValueOrDefault() - 5);
                HttpContext.Session.SetString("Message", $"Worked and gained {gain} Meals!");
            }

            return RedirectToAction("Dojodachi");
        }

        //Sleep action
        [HttpPost("sleep")]
        public IActionResult Sleep()
        {
            
            HttpContext.Session.SetInt32("Energy",  HttpContext.Session.GetInt32("Energy").GetValueOrDefault() + 15);
            HttpContext.Session.SetInt32("Happiness",  HttpContext.Session.GetInt32("Happiness").GetValueOrDefault() - 5);
            HttpContext.Session.SetInt32("Fullness",  HttpContext.Session.GetInt32("Fullness").GetValueOrDefault() - 5);
            HttpContext.Session.SetString("Message", "Slept for a while and regained 15 Energy!");

            return RedirectToAction("Dojodachi");
        }

        [HttpPost("reset")]

        public IActionResult Reset()
        {
            HttpContext.Session.SetInt32("Fullness", 20);
            HttpContext.Session.SetInt32("Happiness", 20);
            HttpContext.Session.SetInt32("Energy", 50);
            HttpContext.Session.SetInt32("Meals", 3);
            HttpContext.Session.SetString("Message", "Game Start!");
            return RedirectToAction("Dojodachi");
        }
    }
}
