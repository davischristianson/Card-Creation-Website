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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Card cardToCreate)
        {
            if(ModelState.IsValid)
            {
                _context.Cards.Add(cardToCreate); // Prepares Insert
                await _context.SaveChangesAsync();

                ViewData["Message"] = $"{cardToCreate.CardName} was added successfully!";
                return View();
            }

            return View(cardToCreate);
        }
    }
}
