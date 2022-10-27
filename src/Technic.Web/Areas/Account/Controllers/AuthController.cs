using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Technic.Web.Areas.Account.Models;
using Technic.Web.Data.Entites;

namespace Technic.Web.Areas.Account.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            var viewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await _userManager.FindByNameAsync(viewModel.Login);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Пользователь не найден");
                return View(viewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(viewModel.Login,
                viewModel.Password, false, false);

            if (result.Succeeded)
            {
                user.LastLogin = DateTime.Now;
                _userManager?.UpdateAsync(user);

                if (!string.IsNullOrEmpty(viewModel.ReturnUrl) && Url.IsLocalUrl(viewModel.ReturnUrl))
                {
                    return Redirect(viewModel.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
            }

            ModelState.AddModelError(string.Empty, "Ошибка входа");

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Register(string? returnUrl)
        {
            var viewModel = new RegisterViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = new User
            {
                RegisteredAt = DateTime.Now,
                Email = viewModel.Email,
                UserName = viewModel.Login
            };

            var result = await _userManager.CreateAsync(user, viewModel.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                if (!string.IsNullOrEmpty(viewModel.ReturnUrl) && Url.IsLocalUrl(viewModel.ReturnUrl))
                {
                    return Redirect(viewModel.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
