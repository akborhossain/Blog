﻿
using BlogWeb.Models.viewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerView) 
        {
            var identityUser = new IdentityUser
            {
                UserName = registerView.Username,
                Email = registerView.Email,

            };
            var identityResult= await userManager.CreateAsync(identityUser,registerView.Password);
            if (identityResult.Succeeded) 
            {
                var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");
                if (roleIdentityResult.Succeeded) 
                {
                    return RedirectToAction("LogIn");
                }
            }
            return View(registerView);
            
        }
    }
}
