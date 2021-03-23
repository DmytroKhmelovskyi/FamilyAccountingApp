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
            if (ModelState.IsValid)
            {
                cardService.Create(MapperService.CardMap(card));
                return RedirectToAction("Index");
            }

            return Content("Invalid inputs");
        }
    }
}
