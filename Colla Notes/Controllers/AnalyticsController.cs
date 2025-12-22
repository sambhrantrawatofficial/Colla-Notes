using Microsoft.AspNetCore.Mvc;

namespace Colla_Notes.Controllers
{
    public class AnalyticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
