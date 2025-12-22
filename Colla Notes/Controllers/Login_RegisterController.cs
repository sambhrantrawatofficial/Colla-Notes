using Colla_Notes.Models;
using Colla_Notes.Services;
using Colla_Notes.Views.Login_Register;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Colla_Notes.Controllers
{
    public class Login_RegisterController : Controller
    {

        private readonly AppDbContext _context;

        public Login_RegisterController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginClass lc)
        {
            if (lc != null)
            {
                var username = lc.Username.ToLower();
                var info = await _context.RegisterClasss.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(username));
                if (info != null)
                {
                    var hasher = new PasswordHasher<RegisterClass>();
                    var result = hasher.VerifyHashedPassword(info, info.Password, lc.Password);
                    if (result == PasswordVerificationResult.Success)
                    {
                        HttpContext.Session.SetString("username", lc.Username);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid username or password");
                        return View(lc);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    return View(lc);
                }
            }
            else
            {
                ModelState.AddModelError("", "Error");
                return View(lc);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterClass rc)
        {
            if (ModelState.IsValid)
            {
                var user = new RegisterClass
                {
                    Name = rc.Name,
                    Password = rc.Password,
                    Confirm_Password = rc.Confirm_Password,
                    Email = rc.Email,
                    Phone_Number = rc.Phone_Number,
                    Username = rc.Username
                };
                var hasher = new PasswordHasher<RegisterClass>();
                user.Password = hasher.HashPassword(user, rc.Password);
                user.Confirm_Password = hasher.HashPassword(user, rc.Confirm_Password);
                _context.RegisterClasss.Add(user);
                await _context.SaveChangesAsync();
                TempData["SuccessMsg"]= "Account created successfully!";

                return RedirectToAction("RegisterSuccessful", "Login_Register");
            }

            return View(rc);
        }


        public IActionResult RegisterSuccessful()
        {
            return View();
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordClass model)
        {
            //under maintainence
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            //under maintainence
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(ResetPasswordClass rpc)
        {
            //under maintainence
            return View();
        }


        [HttpGet]
        public IActionResult ForgetPasswordConfirmation()
        {           
            //under maintainence
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login_Register");
        }
    }
    }
