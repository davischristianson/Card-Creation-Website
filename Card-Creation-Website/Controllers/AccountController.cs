using Azure.Identity;
using Card_Creation_Website.Data;
using Card_Creation_Website.Models;
using Microsoft.AspNetCore.Mvc;
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


        [HttpGet]
        public IActionResult LoginAccount()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteAccount()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DetailsAccount()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UpdateAccount()
        {
            return View();
        }
    }
}
