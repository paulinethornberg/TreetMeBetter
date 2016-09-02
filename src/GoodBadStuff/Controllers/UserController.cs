using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using GoodBadStuff.Models.ViewModels;
using GoodBadStuff.Models;
using GoodBadStuff.Models.Entities;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GoodBadStuff.Controllers
{
    public class UserController : Controller
    {
        UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> _signinManager;
        IdentityDbContext _identityContext;
        DataManager dataManager;
        TrvlrContext _context;

        public UserController(TrvlrContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signinManager, IdentityDbContext dbContext)
        {

            dataManager = new DataManager(context, userManager);
            _userManager = userManager;
            _signinManager = signinManager;
            _identityContext = dbContext;
        }

        [Authorize]
        public async Task<IActionResult> MyTravels()
        {
            var userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var travels = dataManager.LoadTravels(user.Id);
            return View(travels);
        }

        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> MyAccount()
        {
            UserMyAccountVM userMyAccountVm = new UserMyAccountVM();
            var userName = User.Identity.Name;
            userMyAccountVm.Email = await dataManager.GetUserInfoFromdb(userName);

            userMyAccountVm.UserName = userName;

            return View(userMyAccountVm);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> Create(UserCreateVM viewModel)
        {
            var user = new IdentityUser()
            {
                UserName = viewModel.Username,
                Email = viewModel.Email
            };
            var result = await _userManager.CreateAsync(user, viewModel.Password);
            await _signinManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, false, false);
            return result.Succeeded;

        }

        [HttpGet]
        public bool CheckIsLoggedIn()
        {
            bool isLogged = User.Identity.IsAuthenticated;
            return isLogged;
        }


        public async Task<bool> DeleteUser()
        {
            var userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var delete = await _userManager.DeleteAsync(user);

            if (delete.Succeeded)
            {
                await _signinManager.SignOutAsync();
                return delete.Succeeded;
            }
            else
                return false;
        }


        [HttpPost]
        public async Task<bool> CheckPassword(string postContent)
        {
            var userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var check = await _userManager.CheckPasswordAsync(user, postContent);

            return check;
        }

        [Authorize]
        [HttpPost]
        public async Task<bool> UpdateUser(UserMyAccountVM viewModel)
        {

            var currentUsername = User.Identity.Name;
            var isUpdateSuccess = await dataManager.UpdateUserInfo(currentUsername, viewModel);

            return isUpdateSuccess;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> Login(UserLoginVM viewModel)
        {
            var result = await _signinManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, false, false);
            return result.Succeeded;
        }

        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult SadFacts()
        {
            return View();
        }
    }
}
