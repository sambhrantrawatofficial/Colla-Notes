using Colla_Notes.Models;
using Colla_Notes.Services;
using Colla_Notes.Views.Login_Register;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterClass rc)
        {
            if (ModelState.IsValid)
            {
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
        public async Task<IActionResult> ForgetPassword(ForgetPasswordClass fpc)
        {
            if (ModelState.IsValid)
            {
                var token = Guid.NewGuid().ToString();
                
                var resetLink = Url.Action("ResetPassword", "Login_Register",
                    new { token = token, email = fpc.Email }, Request.Scheme);

                var _emailService = new Emailservice();

                _emailService.SendPasswordResetEmail(fpc.Email, resetLink);

                TempData["SuccessMessage"] = "Password reset link has been sent to your email.";
                return RedirectToAction("ForgetPasswordConfirmation");
            }

            return View(fpc);
        }
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
                return BadRequest("Invalid token");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(ResetPasswordClass rpc)
        {
            if (ModelState.IsValid)
            {
                TempData["SuccessMessage"] = "Your password has been reset successfully!";
                return RedirectToAction("Login");
            }

            return View(rpc);
        }


        [HttpGet]
        public IActionResult ForgetPasswordConfirmation()
        {
            return View();
        }

        //public IActionResult Logout()
        //{
        //    HttpContext.Session.Clear();
        //    return RedirectToAction("Index");
        //}




    }
    }
