using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsORM.Models;


namespace SportsORM.Controllers
{
    public class HomeController : Controller
    {

        private static Context _context;

        public HomeController(Context DBContext)
        {
            _context = DBContext;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.BaseballLeagues = _context.Leagues
                .Where(l => l.Sport.Contains("Baseball"))
                .ToList();
            return View();
        }

        [HttpGet("level_1")]
        public IActionResult Level1()
        {
            ViewBag.WomensLeagues = _context.Leagues
            .Where(league => league.Name.Contains("Womens'"))
            .ToList();

            ViewBag.HockeyLeagues = _context.Leagues
            .Where(hl => hl.Sport.Contains("Hockey"))
            .ToList();

            ViewBag.NotFootballLeagues = _context.Leagues
            .Where(l => l.Sport != "Football")
            .ToList();

            ViewBag.ConferenceLeagues = _context.Leagues
            .Where(l => l.Name.Contains("Conference"))
            .ToList();

            ViewBag.AtlanticLeagues = _context.Leagues
            .Where(l => l.Name.Contains("Atlantic"))
            .ToList();

            ViewBag.DallasTeams = _context.Teams
            .Where(t => t.Location.Contains("Dallas"))
            .ToList();

            ViewBag.RaptorTeams = _context.Teams
            .Where(t => t.TeamName.Contains("Raptors"))
            .ToList();

            ViewBag.CityTeams = _context.Teams
            .Where(t => t.Location.Contains("City"))
            .ToList();

            ViewBag.TTeams = _context.Teams
            .Where(t => t.TeamName.StartsWith("T"))
            .ToList();

            ViewBag.LocationOrderedTeams = _context.Teams
            .OrderBy(t => t.Location)
            .ToList();

            ViewBag.ReverseNameOrderedTeams = _context.Teams
            .OrderByDescending(t => t.TeamName)
            .ToList();

            ViewBag.CooperPlayers = _context.Players
            .Where(p => p.LastName.Contains("Cooper"))
            .ToList();

            ViewBag.JoshuaPlayers = _context.Players
            .Where(p => p.FirstName.Contains("Joshua"))
            .ToList();

            ViewBag.CooperNotJoshuaPlayers = _context.Players
            .Where(p => (!(p.FirstName.Contains("Joshua")) && p.LastName.Contains("Cooper")))
            .ToList();

            ViewBag.AlexanderAndWyattPlayers = _context.Players
            .Where(p => (p.FirstName.Contains("Alexander") || p.FirstName.Contains("Wyatt")))
            .ToList();
            return View();


        }

        [HttpGet("level_2")]
        public IActionResult Level2()
        {
            return View();
        }

        [HttpGet("level_3")]
        public IActionResult Level3()
        {
            return View();
        }

    }
}