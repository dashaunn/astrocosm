using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Astrocosm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Astrocosm.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,
                                      SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // Display registration form
        public IActionResult Register()
        {
            return View();
        }


        // Handle adding a new user to the database
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError("", "Invalid Login Attempt");

            }
            return View(model);
        }


        // Display login form
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }


        // Handle logging the user in
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid Login Attempt");

            }
            return View(user);
        }


        // Handle logging the user out
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }


        // Display change email form
        public async Task<ActionResult> ChangeEmailAsync()
        {
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentEmail = user.Email;
            return View();
        }


        // Handle updating the user's email
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeEmail(ChangeEmailViewModel model)
        {
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Update the user's username
            IdentityResult result = await _userManager.SetUserNameAsync(user, model.NewEmail);
            if (result.Succeeded)
            {
                // Update the user's email address and send them to the account page with a success message
                await _userManager.SetEmailAsync(user, model.NewEmail);
                TempData["EmailSuccess"] = "Email update successful";
                return RedirectToAction("Index", "Account");
            }
            else
            {
                ModelState.AddModelError("", "An error occurred while updating the email or username.");
                return View(model);
            }

        }


        // Display change password form
        public ActionResult ChangePassword()
        {
            return View();
        }


        // Handle updating the user's password
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            AppUser user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return NotFound();
            }
            bool isOldPasswordValid = await _userManager.CheckPasswordAsync(user, model.OldPassword);
            if (!isOldPasswordValid)
            {
                ModelState.AddModelError("", "Your entry did not match the current password.");
                return View(model);
            }
            // Update the user's password and send them to the account page with a success message
            IdentityResult result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                TempData["PasswordSuccess"] = "Password update successful";
                return RedirectToAction("Index", "Account");
            }
            else
            {
                ModelState.AddModelError("", "An error occurred while updating the password.");
                return View(model);
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }


    }
}

