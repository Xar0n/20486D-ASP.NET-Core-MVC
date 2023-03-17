using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Library.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Library.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(SignInManager<User> signInManager, 
            UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if(this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Library");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> LoginPost(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, 
                    loginModel.Password, 
                    loginModel.RememberMe, 
                    false);
                if (result.Succeeded) 
                    return RedirectToAction("Index", "Library");

                ModelState.AddModelError("", "Failed to Login");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Library");
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterPost(RegisterViewModel registerModel)
        {
            if(ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = registerModel.UserName,
                    Email = registerModel.Email,
                    PhoneNumber = registerModel.PhoneNumber,
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                };

                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (result.Succeeded)
                {
                    var roleExists = await _roleManager.RoleExistsAsync(registerModel.RoleName);
                    if (!roleExists) await _roleManager.CreateAsync(new IdentityRole(registerModel.RoleName));
                    
                    if (!await _userManager.IsInRoleAsync(user, registerModel.RoleName))
                        await _userManager.AddToRoleAsync(user, registerModel.RoleName);
                    
                    if (!string.IsNullOrWhiteSpace(user.Email))
                    {
                        Claim claim = new Claim(ClaimTypes.Email, user.Email);
                        await _userManager.AddClaimAsync(user, claim);
                    }

                    var resultSignIn = await _signInManager.PasswordSignInAsync(registerModel.UserName, 
                        registerModel.Password, 
                        registerModel.RememberMe, 
                        false);

                    if(resultSignIn.Succeeded)
                        return RedirectToAction("Index", "Library");
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
