using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FamilyAccounting.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService cardService;
        private readonly IWalletService walletService;

        public CardsController(ICardService cardService, IWalletService walletService)
        {
            this.cardService = cardService;
            this.walletService = walletService;

        }
        [HttpGet("{id}")]
        public ActionResult Create(int id)
        {
            CardDTO cardDto = new CardDTO
            {
                WalletId = id
            };
            return new OkObjectResult(cardDto);
        }

        [HttpPost]
        public ActionResult Create(CardDTO card)
        {
            cardService.Create(card);
            return Ok();
        }

        //[HttpGet]
        //public ViewResult Delete(int? id)
        //{
        //    var card = cardService.Get((int)id);
        //    return View(card);
        //}

        [ActionName("Delete")]
        [HttpDelete("{id}")]
        public ActionResult DeleteCard(int? id)
        {
            var card = cardService.Get((int)id);
            cardService.Delete((int)id);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult Update(int id)
        {
            var updatedCard = cardService.Get(id);
            return new OkObjectResult (updatedCard);
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, CardDTO card)
        {
            if (ModelState.IsValid)
            {
                cardService.Update(id, card);
                return Ok();
            }
            return new BadRequestResult();

        }
        [HttpGet("{id}")]
        public ActionResult Details(int Id)
        {
            var card = cardService.Get(Id);
            return new OkObjectResult(card);
        }
    }
}
