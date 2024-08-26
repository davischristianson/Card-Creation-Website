using Microsoft.AspNetCore.Mvc;
using Card_Creation_Website.Models;
using Card_Creation_Website.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Card_Creation_Website.Migrations;


namespace Card_Creation_Website.Controllers
{
    public class CardController : Controller
    {
        private readonly CardCreationContext _context;
        private readonly IEmailProvider _emailProvider;

        public CardController(CardCreationContext context, IEmailProvider emailProvider)
        {
            _context = context;
            _emailProvider = emailProvider;
        }



        public async Task<IActionResult> IndexCard()
        {
            int? accountId = HttpContext.Session.GetInt32("Id");

            List<Card> cards = await _context.Cards.ToListAsync();

            cards.RemoveAll(Card => Card.AccountId != accountId);

            //for (int i = 0; i < tempCards.Count; i++)
            //{
            //    if (tempCards[i].AccountId != accountId)
            //    {
            //        tempCards.RemoveAt(i);

            //    }
            //}

            return View(cards);
        }
        


        [HttpGet]
        public IActionResult CreateCard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCard(Card cardToCreate)
        {
            int? accountId = HttpContext.Session.GetInt32("Id");

            Account accountCard = await _context.Accounts.FindAsync(accountId);

            cardToCreate.Account = accountCard;
            cardToCreate.AccountId = accountCard.AccountId;

            // To ignore the ModelState validation on the form data for the 
            // Card class account property. This helps ignore the error that occurs since
            // that specific property always gets treated as null even if its added
            // to the card object being created. 
            if (cardToCreate.Account != null)
            {
                ModelState.Remove("Account");
            }

            if (ModelState.IsValid)
            {
                // To send a congratulations on the first card that was created associated with that account!
                List<Card> cards = await _context.Cards.ToListAsync();

                cards.RemoveAll(Card => Card.AccountId != accountId);

                if (cards.Count == 0)
                {
                    // Email cannot be set just yet since legit emails aren't setup yet
                    // string email = accountCard.Email
                    // fromEmail must remain null since we don't want to be changing the sender yet
                    // Subject can be implemented
                    string subject = "First Card Made!";
                    // Content can be implemented
                    string content = "Congrats on making your card, and hopefully you make more to come. We " +
                        "appreciate you using our service and make some really cool cards!";
                    // htmlContent can be implemented
                    string htmlContent = "<strong>We promise the best service! Please share any feedback you might have.<strong>";
                    // Later once name is added to the method SendEmailAsync();
                    // string fullName = accountCard.FirstName + " " + accountCard.LastName

                    await _emailProvider.SendEmailAsync(null, null, subject, content, htmlContent);
                }


                _context.Cards.Add(cardToCreate);
                await _context.SaveChangesAsync();

                ViewData["Message"] = $"{cardToCreate.CardName} was added successfully!";
                return RedirectToAction("IndexCard");
            }

            return View(cardToCreate);
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