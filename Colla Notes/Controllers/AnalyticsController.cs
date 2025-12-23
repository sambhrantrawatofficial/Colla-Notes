using Colla_Notes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colla_Notes.Controllers
{
    public class AnalyticsController : Controller
    {
        private readonly AppDbContext db;
        public AnalyticsController(AppDbContext appDbContext)
        {
            db = appDbContext;
        }
        public IActionResult Index()
        {
            string Username = HttpContext.Session.GetString("username");

            if (string.IsNullOrEmpty(Username))
                return RedirectToAction("Login", "Login_Register");

            var posts = db.Posts
                .Where(x => x.Username == Username)
                .ToList();

            // Totals
            ViewBag.TotalPosts = posts.Count;
            ViewBag.TotalLikes = posts.Sum(x => x.LikeCount);
            ViewBag.TotalViews = posts.Sum(x => x.ViewCount);

            // Count comments (split by ||)
            ViewBag.TotalComments = posts.Sum(p =>
                string.IsNullOrEmpty(p.Comments)
                    ? 0
                    : p.Comments.Split("||").Length
            );

            return View(posts);
        }
    }
}
