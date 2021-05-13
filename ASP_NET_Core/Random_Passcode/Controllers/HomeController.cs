using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Random_Passcode.Models;
using Microsoft.AspNetCore.Http;

namespace Random_Passcode.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet("")]
        public IActionResult Index(){
            HttpContext.Session.SetInt32("Count", 1);
            ViewBag.GenCount = HttpContext.Session.GetInt32("Count");
            return View("RandomPass");
        }

        [HttpPost("")]

        public IActionResult Generate(){
            HttpContext.Session.SetInt32("Count", (HttpContext.Session.GetInt32("Count").GetValueOrDefault() +1));
            ViewBag.GenCount = HttpContext.Session.GetInt32("Count");

            return View("RandomPass");
        }
    }
}
