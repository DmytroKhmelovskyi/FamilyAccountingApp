using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Models;
using FamilyAccounting.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace FamilyAccounting.Web.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardService cardService;

        public CardController(ICardService cardService)
        {
            this.cardService = cardService;
        }
        public IActionResult Create()
        {
            return View();
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
    }
}
