using System.Diagnostics;
using Colla_Notes.Models;
using Microsoft.AspNetCore.Mvc;

namespace Colla_Notes.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Readmore(int id)
        {
            if (id == 1)
            {
                ViewBag.Title = "HTML";
                ViewBag.Description =
                    "HTML (HyperText Markup Language) is the foundational language used to structure content on the web.";
            }
            else if (id == 2)
            {
                ViewBag.Title = "CSS";
                ViewBag.Description =
                    "CSS (Cascading Style Sheets) controls the appearance and layout of web pages.";
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Readmore()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
