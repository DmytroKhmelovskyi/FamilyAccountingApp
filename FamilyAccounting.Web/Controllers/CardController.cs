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
            try
            {
                CardViewModel cardViewModel = new CardViewModel
                {
                    WalletId = id
                };

                return View(cardViewModel);
            }            
            catch(Exception)
            {
                throw new Exception("Exception");
            }
        }

        [HttpPost]
        public IActionResult Create(CardViewModel card)
        {
            try
            {
                cardWebService.Create(card);
                return RedirectToAction("Details", "Wallet", new { id = card.WalletId });
            }
            catch(Exception)
            {
                throw new Exception("Exception");
            }
        }

        [HttpGet]
        public ViewResult Delete(int? id)
        {
            try
            {
                var card = cardWebService.Get((int)id);
                return View(card);
            }
            catch (Exception)
            {
                throw new Exception("Exception");
            }
        }

        [ActionName("Delete")]
        [HttpPost]
        public IActionResult DeleteWallet(int? id)
        {
            try
            {
                cardWebService.Delete((int)id);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw new Exception("Exception");
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            try
            {
                var updatedCard = cardWebService.Get(id);
                return View(updatedCard);
            }
            catch (Exception)
            {
                throw new Exception("Exception");
            }
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
            try
            {
                var card = cardWebService.Get(Id);
                return View(card);
            }
            catch (Exception)
            {
                throw new Exception("Exception");
            }
        }
    }
}
