using Colla_Notes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Colla_Notes.Controllers
{
    public class PostsController : Controller
    {
        private readonly AppDbContext _context;
        public PostsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult PostHome()
        {
            var posts = _context.Posts.Where(x => x.Username.ToLower().Equals(HttpContext.Session.GetString("username").ToLower())).ToList();
            // Load likes & comments from session
            foreach (var post in posts)
            {
                post.LikeCount =
                    HttpContext.Session.GetInt32($"Post_{post.Post_Id}_Likes") ?? 0;
                post.Comments =
                    HttpContext.Session.GetString($"Post_{post.Post_Id}_Comments");
            }
            return View(posts);
        }

        [HttpGet]
        public IActionResult PostCreate()
        {
            return View(new Post());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostCreate(Post model)
        {
            if (ModelState.IsValid)
            {
                var post = new Post
                {
                    Title = model.Title,
                    Description = model.Description,
                    Comments = model.Comments,
                    LikeCount = model.LikeCount,
                    Username = HttpContext.Session.GetString("username"),
                    ViewCount = model.ViewCount,
                    Tag_1 = model.Tag_1,
                    Tag_2 = model.Tag_2
                };
                await _context.Posts.AddAsync(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(PostHome));
            }
            return RedirectToAction(nameof(PostHome));
        }

        // ================= EDIT =================
        [HttpGet]
        public IActionResult PostEdit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();
            var post = _context.Posts.FirstOrDefault(x => x.Post_Id == id);
            if (post == null)
                return NotFound();
            return View(post); // ✅ single Post
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PostEdit(Post model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var post = _context.Posts.FirstOrDefault(x => x.Post_Id == model.Post_Id);
            if (post == null)
                return NotFound();
            post.Title = model.Title;
            post.Description = model.Description;
            post.Tag_1 = model.Tag_1;
            post.Tag_2 = model.Tag_2;
            post.Updated_on = DateTime.Now.ToString("dd-MMM-yyyy:hh:mm");
            _context.SaveChanges();
            return RedirectToAction(nameof(PostHome));
        }

        // ================= DELETE =================
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var data = _context.Posts.FirstOrDefault(x => x.Post_Id.Equals(id));
            if (data != null)
            {
                _context.Posts.Remove(data);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(PostHome));
        }
    }


}

