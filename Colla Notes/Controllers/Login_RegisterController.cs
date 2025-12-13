using Microsoft.AspNetCore.Mvc;

namespace Colla_Notes.Controllers
{
    public class Login_RegisterController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
