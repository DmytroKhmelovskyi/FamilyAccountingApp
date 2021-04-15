using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FamilyAccounting.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService cardService;

        public CardsController(ICardService cardService)
        {
            this.cardService = cardService;

        }

        [HttpPost]
        public async Task<ActionResult> Create(CardDTO card)
        {
            return new OkObjectResult(await cardService.CreateAsync(card));
        }

        [ActionName("Delete")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCard(int id)
        {
            await cardService.DeleteAsync(id);
            return new OkResult();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CardDTO card)
        {
            if (ModelState.IsValid)
            {
                return new OkObjectResult(await cardService.UpdateAsync(id, card));
            }
            return new BadRequestResult();

        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Details(int Id)
        {
            return new OkObjectResult(await cardService.GetAsync(Id));
        }
    }
}
