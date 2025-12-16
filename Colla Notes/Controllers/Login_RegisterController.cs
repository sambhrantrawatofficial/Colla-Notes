using Colla_Notes.Models;
using Colla_Notes.Views.Login_Register;
using Microsoft.AspNetCore.Mvc;

namespace Colla_Notes.Controllers
{
    public class Login_RegisterController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginClass lc)
        {
            Console.WriteLine(lc.Email);
            Console.WriteLine(lc.Password);

            HttpContext.Session.SetString("email", lc.Email);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }
    }

}
