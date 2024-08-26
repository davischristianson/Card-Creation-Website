using Microsoft.AspNetCore.Mvc;
using Card_Creation_Website.Models;
using Card_Creation_Website.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Drawing;
using Card_Creation_Website.Services;
using Azure.Storage.Blobs;


namespace Card_Creation_Website.Controllers
{
    public class CardController : Controller
    {
        private readonly CardCreationContext _context;
        private readonly AzureBlobService _blobService;

        public CardController(CardCreationContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> IndexCard()
        {
            int? accountId = HttpContext.Session.GetInt32("Id");

            List<Card> cards = await _context.Cards.ToListAsync();

            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].AccountId != accountId)
                {
                    cards.RemoveAt(i);
                }
            }

            return View(cards);
        }
        


        [HttpGet]
        public IActionResult CreateCard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCard(CombinedModel cardToCreate)
        {
            int? accountId = HttpContext.Session.GetInt32("Id");

            Account accountCard = await _context.Accounts.FindAsync(accountId);

            cardToCreate.CardModel.Account = accountCard;
            cardToCreate.CardModel.AccountId = accountCard.AccountId;

            // To ignore the ModelState validation on the form data for the 
            // Card class account property. This helps ignore the error that occurs since
            // that specific property always gets treated as null even if its added
            // to the card object being created. 
            if (cardToCreate.CardModel.Account != null)
            {
                ModelState.Remove("Account");
            }

            if (ModelState.IsValid)
            {
                _context.Cards.Add(cardToCreate.CardModel);
                await _context.SaveChangesAsync();

                ViewData["Message"] = $"{cardToCreate.CardModel.CardName} was added successfully!";
                return RedirectToAction("IndexCard");
            }

            return View(cardToCreate.CardModel);
        }



        public async Task<IActionResult> UpdateCard(int id)
        {
            Card cardToCreate = await _context.Cards.FindAsync(id);

            if(cardToCreate == null)
            {
                return NotFound();
            }

            return View(cardToCreate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCard(Card cardModel)
        {
            int? accountId = HttpContext.Session.GetInt32("Id");
            cardModel.AccountId = (int)accountId;
            Account accountCard = await _context.Accounts.FindAsync(accountId);
            cardModel.Account = accountCard;


            if (cardModel.AccountId != null)
            {
                ModelState.Remove("Account");
            }

            if (ModelState.IsValid)
            {
                //Account accountCard = await _context.Accounts.FindAsync(accountId);

                //cardModel.Account = accountCard;

                _context.Cards.Update(cardModel);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"{cardModel.CardName} was updated successfully!";
                return RedirectToAction("IndexCard");
            }
            return RedirectToAction("DetailsCard");
        }



        public async Task<IActionResult> DeleteCard(int id)
        {
            Card? cardToDelete = await _context.Cards.FindAsync(id);

            if(cardToDelete == null)
            {
                return NotFound();
            }

            return View(cardToDelete);
        }

        [HttpPost, ActionName("DeleteCard")] 
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Card cardToDelete = await _context.Cards.FindAsync(id);

            if(cardToDelete != null)
            {
                _context.Cards.Remove(cardToDelete);
                await _context.SaveChangesAsync();
                TempData["Message"] = cardToDelete.CardName + " was deleted successfully!";
                return RedirectToAction("IndexCard");
            }

            TempData["Message"] = "This card was already deleted!";
            return RedirectToAction("IndexCard");
        }



        public async Task<IActionResult> DetailsCard(int id)
        {
            Card? cardDetails = await _context.Cards.FindAsync(id);

            if(cardDetails == null)
            {
                return NotFound();
            }

            return View(cardDetails);
        }
    }
}