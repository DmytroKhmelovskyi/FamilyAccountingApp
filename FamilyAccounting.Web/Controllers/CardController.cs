using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Models;
using FamilyAccounting.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace FamilyAccounting.Web.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardService cardService;
        private readonly IWalletService walletService;

        public CardController(ICardService cardService, IWalletService walletService)
        {
            this.cardService = cardService;
            this.walletService = walletService;

        }
        public IActionResult Create(int id)
        {
            CardViewModel cardViewModel = new CardViewModel {
                WalletId = id
            };

            return View(cardViewModel);
        }

        [HttpPost]
        public IActionResult Create(CardViewModel card)
        {
            cardService.Create(MapperService.CardMap(card));
            return RedirectToAction("Details", "Wallet", new { id = card.WalletId });
        }

        [HttpGet]
        public ViewResult Delete(int? id)
        {
            var card = cardService.Get((int)id);
            return View(MapperService.CardMap(card));
        }

        [ActionName("Delete")]
        [HttpPost]
        public IActionResult DeleteWallet(int? id)
        {
            cardService.Delete((int)id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var updatedCard = cardService.Get(id);
            return View(MapperService.CardMap(updatedCard));
        }

        [HttpPost]
        public IActionResult Update(int id, CardViewModel card)
        {
            if (ModelState.IsValid)
            {
                cardService.Update(id, MapperService.CardMap(card));
            }
            return RedirectToAction("Details", "Wallet", new { id = card.WalletId });
        }
        public IActionResult Details(int Id)
        {
            CardDTO card = cardService.Get(Id);
            return View(MapperService.CardMap(card));
        }
    }
}
