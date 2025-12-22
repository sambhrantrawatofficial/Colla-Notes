using System.Diagnostics;
using Colla_Notes.Models;
using Microsoft.AspNetCore.Mvc;

namespace Colla_Notes.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var posts = _context.Posts.ToList();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Readmore(string id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Post_Id == id);
            if (post == null) return NotFound();

            string viewCountKey = $"Post_{id}_Views";
            string viewedKey = $"Post_{id}_Viewed";

            // Only increment view count once per session
            if (HttpContext.Session.GetString(viewedKey) == null)
            {
                HttpContext.Session.SetString(viewedKey, "true");
                post.ViewCount += 1;
                _context.SaveChanges();
            }

            ViewBag.Post_Id = id;
            ViewBag.Title = post.Title;
            ViewBag.Description = post.Description;
            ViewBag.Views = post.ViewCount;
            ViewBag.Likes = post.LikeCount;
            ViewBag.Tag_1 = post.Tag_1;
            ViewBag.Tag_2 = post.Tag_2;
            ViewBag.AlreadyLiked = HttpContext.Session.GetString($"Post_{id}_Liked") != null;

            // Parse comments
            var commentList = new List<dynamic>();
            if (!string.IsNullOrEmpty(post.Comments))
            {
                var comments = post.Comments.Split("||");
                foreach (var c in comments)
                {
                    var parts = c.Split("~");
                    if (parts.Length == 3)
                    {
                        commentList.Add(new
                        {
                            UserName = parts[0],
                            Text = parts[1],
                            Date = parts[2]
                        });
                    }
                }
            }
            ViewBag.Comments = commentList;

            return View();
        }

        // ================= LIKE (ONLY ONE LIKE PER USER) =================
        [HttpPost]
        public IActionResult Like(string id)
        {
            string likedKey = $"Post_{id}_Liked";

            var post = _context.Posts.FirstOrDefault(x => x.Post_Id.Equals(id));

            // Allow only one like per session
            if (post != null && HttpContext.Session.GetString(likedKey) == null)
            {
                post.LikeCount = post.LikeCount + 1;
                _context.SaveChanges();
                HttpContext.Session.SetString(likedKey, "true");
            }

            return RedirectToAction(nameof(Readmore), new { id });
        }

        // ================= ADD COMMENT =================
        [HttpPost]
        public IActionResult AddComment(string id, string commentText)
        {
            if (string.IsNullOrWhiteSpace(commentText))
                return RedirectToAction(nameof(Readmore), new { id });

            var post = _context.Posts.FirstOrDefault(x => x.Post_Id == id);
            if (post != null)
            {
                // FIX: Changed from "UserName" to "username" to match Login controller
                string userName = HttpContext.Session.GetString("username") ?? "Guest";
                string dateTime = DateTime.Now.ToString("dd MMM yyyy hh:mm tt");
                string newComment = $"{userName}~{commentText}~{dateTime}";

                if (string.IsNullOrEmpty(post.Comments))
                    post.Comments = newComment;
                else
                    post.Comments += "||" + newComment;

                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Readmore), new { id });
        }

        public IActionResult Privacy()
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