using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroBlog.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MicroBlog.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login obj)
        {
            if (ModelState.IsValid)
            {
                
                var user = await _userManager.FindByNameAsync(obj.username);

                // If user has found
                if (user != null)
                {
                    // Login
                    var result = await _signInManager.PasswordSignInAsync(user, obj.password, false, false);

                   
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                // Error message
                ModelState.AddModelError("", "Kullanıcı Bulunamadı.");
                return View(obj);
            }
            return View(obj);
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Login obj)
        {
            if (ModelState.IsValid)
            {
                // Creating new user
                var user = new IdentityUser()
                { UserName = obj.username };
                var result = await _userManager.CreateAsync(user, obj.password);

               
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("", "Your password must contain at least one number digit, one uppercase, one lowercase. ");
                return View(obj);
            }
            return View(obj);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}