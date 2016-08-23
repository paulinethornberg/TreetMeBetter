using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using GoodBadStuff.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GoodBadStuff.Controllers
{
    public class UserController : Controller
    {
        UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> _signinManager;
        IdentityDbContext _identityContext;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signinManager, IdentityDbContext dbContext)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _identityContext = dbContext;
        }

        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(UserCreateVM viewModel)
        {
            await _identityContext.Database.EnsureCreatedAsync();
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var result = await _userManager.CreateAsync(new IdentityUser(viewModel.Username), viewModel.Password);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(nameof(UserCreateVM.Username), result.Errors.First().Description);
                return View(viewModel);
            }

            await _signinManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, false, false);

            return RedirectToAction("index", "cars");

        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var result = await _signinManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(nameof(UserLoginVM.Username), "Login failed");
                return View(viewModel);
            }
            return RedirectToAction(nameof(HomeController.Index), "index");
        }

        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction(nameof(UserController.Login));
        }
    }
}
