﻿using System;
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
        DataManager _dataManager;
        

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signinManager, IdentityDbContext dbContext)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _identityContext = dbContext;
        }
        public async Task<IActionResult> MyTravels()
        {
            var userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            _dataManager = new DataManager(new TrvlrContext(), _userManager);
            var travels = _dataManager.LoadTravels(user.Id);
            return View(travels);
        }

        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> MyAccount()
        {
            UserMyAccountVM userMyAccountVm = new UserMyAccountVM();
            var userName = User.Identity.Name;
            _dataManager = new DataManager(new TrvlrContext(), _userManager);
            var email = await _dataManager.GetUserInfoFromdb(userName);
            userMyAccountVm.Email = email;
            userMyAccountVm.UserName = userName;
            
            return View(userMyAccountVm);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> Create(UserCreateVM viewModel)
        {
            var result = await _userManager.CreateAsync(new IdentityUser(viewModel.Username), viewModel.Password);
            await _signinManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, false, false);
            return result.Succeeded;

        }


        [HttpGet]
        public bool CheckIsLoggedIn()
        {
            bool isLogged = User.Identity.IsAuthenticated;
            return isLogged;
        }
        [HttpPost]
        public async Task<bool> DeleteUser()
        {
           
            var userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var delete = await _userManager.DeleteAsync(user);

            if (delete.Succeeded)
                return true;
            else
                return false;
        }

        [HttpPost]
        public async Task<bool> CheckPassword(string postContent)
        {
            var userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var check =  await _userManager.CheckPasswordAsync(user, postContent);
           
            return check;
        }

        [HttpPost]
        public async Task<bool> UpdateUser(string Username, string CurrentPassword, string Password, string Email)
        {
            var currentUsername = User.Identity.Name;
            var currentUser = await _userManager.FindByNameAsync(currentUsername);

            currentUser.UserName = Username;
            currentUser.Email = Email;

            await _userManager.UpdateAsync(currentUser);
            //var emailConfirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(currentUser);
            //var emailCheck= await _userManager.ChangeEmailAsync(currentUser, Email, emailConfirmationCode);

            var passwordChange = await _userManager.ChangePasswordAsync(currentUser, CurrentPassword, Password);
            if (passwordChange.Succeeded)
                return true;
            else
                return false;

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
