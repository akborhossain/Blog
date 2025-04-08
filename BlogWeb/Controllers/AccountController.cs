
using BlogWeb.Models.viewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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
        [HttpGet]
        public IActionResult Login( string ReturnUrl) 
        {
            var model = new LogInViewModel { ReturnUrl = ReturnUrl };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult>Login(LogInViewModel logInViewModel)
        {
            var signInResult= await signInManager.PasswordSignInAsync(logInViewModel.Username, logInViewModel.Password, false, false);
            if (signInResult != null && signInResult.Succeeded) 
            {
                if (!string.IsNullOrWhiteSpace(logInViewModel.ReturnUrl))
                {
                    return Redirect(logInViewModel.ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
