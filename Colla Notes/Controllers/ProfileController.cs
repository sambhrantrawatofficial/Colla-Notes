using Microsoft.AspNetCore.Mvc;

namespace Colla_Notes.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
