using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FamilyAccounting.Web.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardWebService cardWebService;
        private readonly IWalletWebService walletWebService;

        public CardController(ICardWebService cardWebService, IWalletWebService walletWebService)
        {
            this.cardWebService = cardWebService;
            this.walletWebService = walletWebService;

        }
        public IActionResult Create(int id)
        {
            CardViewModel cardViewModel = new CardViewModel
            {
                WalletId = id
            };
            return View(cardViewModel);
        }

        [HttpPost]
        public IActionResult Create(CardViewModel card)
        {
                cardWebService.Create(card);
                return RedirectToAction("Details", "Wallet", new { id = card.WalletId });
        }

        [HttpGet]
        public ViewResult Delete(int? id)
        {

                var card = cardWebService.Get((int)id);
                return View(card);

        }

        [ActionName("Delete")]
        [HttpPost]
        public IActionResult DeleteCard(int? id)
        {

             var card = cardWebService.Get((int)id);
            cardWebService.Delete((int)id);
            return RedirectToAction("Details", "Wallet", new { id = card.WalletId });

        }

        [HttpGet]
        public IActionResult Update(int id)
        {

                var updatedCard = cardWebService.Get(id);
                return View(updatedCard);

        }

        [HttpPost]
        public IActionResult Update(int id, CardViewModel card)
        {
            if (ModelState.IsValid)
            {
                cardWebService.Update(id,card);
            }
            return RedirectToAction("Details", "Card", new { id = card.WalletId });
        }
        public IActionResult Details(int Id)
        {

                var card = cardWebService.Get(Id);
                return View(card);

        }
    }
}
