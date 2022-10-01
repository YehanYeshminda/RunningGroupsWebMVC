using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunningGroupsWeb.Data;
using RunningGroupsWeb.Models;
using RunningGroupsWeb.ViewModel;

namespace RunningGroupsWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;

        }

        public IActionResult Login()
        {
            var responce = new LoginViewModel();
            return View(responce);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if (user != null)
            {
                // user is present
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);

                if (passwordCheck)
                {
                    // password is correct
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                    if (result.Succeeded)
                    {
                        // if the password and the email is correct
                        return RedirectToAction("Index", "Home");
                    }
                }

                // if password is wrong 
                TempData["Error"] = "Wrong Credentials! Try Again!";
                return View(loginViewModel);
            }
            // user not found
            TempData["Error"] = "Wrong credentials! Try Again!";
            return View(loginViewModel);
        }

        public IActionResult Register()
        {
            var responce = new RegisterViewModel();
            return View(responce);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);

            if (user != null)
            {
                TempData["Error"] = "This email is already in use!";
                return View(registerViewModel);
            }

            var newUserEmail = new AppUser()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress
            };

            var newUserResponce = await _userManager.CreateAsync(newUserEmail, registerViewModel.Password);

            if (newUserResponce.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUserEmail, UserRoles.User);
            }

            return RedirectToAction("Login", "Account");
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
