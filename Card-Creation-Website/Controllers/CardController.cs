using Microsoft.AspNetCore.Mvc;
using Card_Creation_Website.Models;
using Card_Creation_Website.Data;
using Microsoft.EntityFrameworkCore;


namespace Card_Creation_Website.Controllers
{
    public class CardController : Controller
    {
        private readonly CardCreationContext _context;

        public CardController(CardCreationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexCard()
        {
            List<Card> cards = await _context.Cards.ToListAsync();

            return View(cards);
        }

        [HttpGet]
        public IActionResult CreateCard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UpdateCard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteCard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DetailsCard()
        {
            return View();
        }
    }
}
