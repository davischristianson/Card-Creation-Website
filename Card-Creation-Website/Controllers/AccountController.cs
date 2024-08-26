using Azure.Identity;
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
        private readonly IEmailProvider _emailProvider;

        public AccountController(CardCreationContext context, IEmailProvider emailProvider)
        {
            _context = context;
            _emailProvider = emailProvider;
        }



        [HttpGet]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(RegisterViewModel registerViewModel, Account id)
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

                

                //Account loggedUser = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == registerViewModel.Email);

                //int accountId = loggedUser.AccountId;

                LogUserIn(registerViewModel.Email, newAccount.AccountId);
            }

            // Email cannot be set just yet since legit emails aren't setup yet
            // string email = accountCard.Email
            // fromEmail must remain null since we don't want to be changing the sender yet
            // Subject can be implemented
            string subject = "Welcome from Card Creation Website!";
            // Content can be implemented
            string content = "Congrats on registering with us, we here at the team are delighted to have you join us!" +
                " We look forward to the wonderful cards you create!";
            // htmlContent can be implemented
            string htmlContent = "<strong>We promise the best service! Please share any feedback you might have.<strong>";
            // Later once name is added to the method SendEmailAsync();
            // string fullName = accountCard.FirstName + " " + accountCard.LastName

            await _emailProvider.SendEmailAsync(null, null, subject, content, htmlContent);

            // Redirect to home page
            return RedirectToAction("Index", "Home");
        }

        private void LogUserIn(string email, int id)
        {
            HttpContext.Session.SetString("Email", email);
            HttpContext.Session.SetInt32("Id", id);
        }



        [HttpGet]
        public IActionResult LoginAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAccount(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                // Check Db for credentials
                Account? a = (from account in _context.Accounts
                              where account.Email == loginViewModel.Email &&
                              account.Password == loginViewModel.Password
                              select account).SingleOrDefault();

                Account loggedUser = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == loginViewModel.Email);


                // If the account exists, send to the home page
                if (a != null)
                {
                    int accountId = loggedUser.AccountId;
                    LogUserIn(loginViewModel.Email, accountId);
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

                LogoutAccount();

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }




        [HttpGet]
        public async Task<IActionResult> DetailsAccount()
        {
            int? accountId = HttpContext.Session.GetInt32("Id");

            Account? accountDetails = await _context.Accounts.FindAsync(accountId);

            if (accountDetails == null)
            {
                return NotFound();
            }

            return View(accountDetails);
        }



        public async Task<IActionResult> UpdateAccount(int id)
        {
            Account accountToEdit = await _context.Accounts.FindAsync(id);

            if (accountToEdit == null)
            {
                return NotFound();
            }

            return View(accountToEdit);
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
