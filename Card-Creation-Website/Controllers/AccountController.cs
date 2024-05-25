﻿using Azure.Identity;
using Card_Creation_Website.Data;
using Card_Creation_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Card_Creation_Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly CardCreationContext _context;

        public AccountController(CardCreationContext context)
        {
            _context = context;
        }



        [HttpGet]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                // Map the RegisterViewModel data to Account Object
                Account newAccount = new Account()
                {
                    Username = registerViewModel.Username,
                    Email = registerViewModel.Email,
                    Password = registerViewModel.Password,
                };

                _context.Accounts.Add(newAccount);
                await _context.SaveChangesAsync();

                LogUserIn(registerViewModel.Email);
            }

            // Redirect to home page
            return RedirectToAction("Index", "Home");
        }

        private void LogUserIn(string email)
        {
            HttpContext.Session.SetString("Email", email);
        }



        [HttpGet]
        public IActionResult LoginAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginAccount(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                // Check Db for credentials
                Account? a = (from account in _context.Accounts
                              where account.Email == loginViewModel.Email &&
                              account.Password == loginViewModel.Password
                              select account).SingleOrDefault();

                // If the account exists, send to the home page
                if (a != null)
                {
                    LogUserIn(loginViewModel.Email);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Credentials not found!");
            }
            // Return if no record found, or the ModelState is invalid
            return View(loginViewModel);
        }



        [HttpGet]
        public IActionResult LogoutAccount()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }



        public async Task<IActionResult> DeleteAccount(int id)
        {
            Account? accountToDelete = await _context.Accounts.FindAsync(id);

            if (accountToDelete == null)
            {
                return NotFound();
            }

            return View(accountToDelete);
        }

        [HttpPost, ActionName("DeleteAccount")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Account accountToDelete = await _context.Accounts.FindAsync(id);

            if (accountToDelete != null)
            {
                _context.Accounts.Remove(accountToDelete);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }




        [HttpGet]
        public async Task<IActionResult> DetailsAccount(int id)
        {
            Account? accountDetails = await _context.Accounts.FindAsync(id);

            if (accountDetails == null)
            {
                return NotFound();
            }

            return View(accountDetails);
        }



        [HttpGet]
        public async Task<IActionResult> UpdateAccount(int id)
        {
            Account accountToEdit = await _context.Accounts.FindAsync(id);

            if (accountToEdit == null)
            {
                return NotFound();
            }

            return View(id);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAccount(Account accountModel)
        {
            if(ModelState.IsValid)
            {
                _context.Accounts.Update(accountModel);
                await _context.SaveChangesAsync();

                return RedirectToAction("DetailsAccount", "Account");
            }

            return View(accountModel);
        }
    }
}
