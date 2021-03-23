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

            WalletViewModel walletViewModel = new WalletViewModel
            {
                Card = cardViewModel
            };
            return View(cardViewModel);
        }

        [HttpPost]
        public IActionResult Create(CardViewModel card)
        {
            cardService.Create(MapperService.CardMap(card));
            return RedirectToAction("Details", "Wallet", new { id = card.WalletId });
        }
    }
}
