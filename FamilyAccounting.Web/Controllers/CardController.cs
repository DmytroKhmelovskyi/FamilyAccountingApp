using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Create(CardViewModel card)
        {
            await cardWebService.Create(card);
            return RedirectToAction("Details", "Wallet", new { id = card.WalletId });
        }

        [HttpGet]
        public async Task<ViewResult> Delete(int? id)
        {
            var card = await cardWebService.Get((int)id);
            return View(card);
        }

        [ActionName("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteCard(int? id)
        {
            var card = await cardWebService.Get((int)id);
           await cardWebService.Delete((int)id);
            return RedirectToAction("Details", "Wallet", new { id = card.WalletId });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var updatedCard = await cardWebService.Get(id);
            return View(updatedCard);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, CardViewModel card)
        {
            if (ModelState.IsValid)
            {
               await cardWebService.Update(id,card);
            }
            return RedirectToAction("Details", "Card", new { id = card.WalletId });
        }
        public async Task<IActionResult> Details(int Id)
        {
            var card = await cardWebService.Get(Id);
            return View(card);
        }
    }
}
