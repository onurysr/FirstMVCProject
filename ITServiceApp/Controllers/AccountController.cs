using ITServiceApp.Models;
using ITServiceApp.Models.Identity;
using ITServiceApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITServiceApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            CheckRole();
        }

        private void CheckRole()
        {
            foreach (var roleName in RoleModels.Roles)
            {
                if (!_roleManager.RoleExistsAsync(roleName).Result)
                {
                    var result = _roleManager.CreateAsync(new ApplicationRole()
                    {
                        Name = roleName
                    }).Result;                   
                }
            }
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.UserName); //await(asenkron methodların çalışması için kullanılır) çalışması için methodun async olması ve task IActionresult olması lazım

            if (user != null)
            {
                ModelState.AddModelError(nameof(model.UserName), "Bu kullanıcı adı daha önce kayıt edilmişir.");
                return View(model);
            }

            user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                ModelState.AddModelError(nameof(model.Email), "Bu E-mail hesabı daha önce kayıt edilmiştir.");
                return View(model);
            }

            user = new ApplicationUser()
            {
                Email = model.Email,
                Name = model.Name,
                UserName = model.UserName,
                Surname = model.Surname
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //kullanıcıya rol atama
                var count = _userManager.Users.Count();
                result = await _userManager.AddToRoleAsync(user, count == 1 ? RoleModels.Admin : RoleModels.User);
                //Email onay maili
                //Login sayfasına Yönlendirme
                return RedirectToAction("Login","Account");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Bir hata oluştu");
                return View(model);
            }
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı Adı veya Şifre Hatalı");
                return View(model);
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
