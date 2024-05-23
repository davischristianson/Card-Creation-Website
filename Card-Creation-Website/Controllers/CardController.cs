﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<IActionResult> CreateCard(Card cardToCreate)
        {
            if(ModelState.IsValid)
            {
                _context.Cards.Add(cardToCreate);
                await _context.SaveChangesAsync();

                ViewData["Message"] = $"{cardToCreate.CardName} was added successfully!";
                return View();
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
            if(ModelState.IsValid)
            {
                _context.Cards.Update(cardModel);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"{cardModel.CardName} was updated successfully!";
                return RedirectToAction("IndexCard");
            }
            return View(cardModel);
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

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Card cardToDelete = await _context.Cards.FindAsync(id);

            if(cardToDelete == null)
            {
                _context.Cards.Remove(cardToDelete);
                await _context.SaveChangesAsync();
                TempData["Message"] = cardToDelete.CardName + " was deleted successfully!";
                return RedirectToAction("IndexCard");
            }

            TempData["Message"] = "This card was already deleted!";
            return RedirectToAction("IndexCard");
        }



        public async Task<IActionResult> Details(int id)
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