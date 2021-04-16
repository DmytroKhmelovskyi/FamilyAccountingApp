using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyAccounting.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletService walletService;
        private readonly IPersonService personService;

        public WalletsController(IWalletService walletService, IPersonService personService)
        {
            this.walletService = walletService;
            this.personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult> GetWallets()
        {
            return new OkObjectResult(await walletService.GetAsync());
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> MakeActive(int Id)
        {
            await walletService.MakeActiveAsync(Id);
            return new OkResult();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Details(int Id)
        {
            var wallet = await walletService.GetAsync(Id);
            wallet.Transactions = await walletService.GetTransactionsAsync(Id);
            return new OkObjectResult(wallet);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Update(int id)
        {
            return new OkObjectResult(await walletService.GetAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, WalletDTO wallet)
        {
            if (ModelState.IsValid)
            {
                await walletService.UpdateAsync(id, wallet);
                return new OkResult();
            }
            return Content("Invalid inputs");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            return new OkObjectResult(await walletService.GetAsync((int)id));
        }

        [ActionName("Delete")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWallet(int? id)
        {
            var wallet = walletService.GetAsync((int)id);
            await walletService.DeleteAsync((int)id);
            return new OkResult();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Create(int id)
        {
            var person = await personService.GetAsync(id);
            WalletDTO walletVM = new WalletDTO
            {
                PersonId = person.Id
            };
            return new OkObjectResult(walletVM);
        }

        [HttpPost]
        public async Task<ActionResult> Create(WalletDTO wallet)
        {
            await walletService.CreateAsync(wallet);
            return new OkResult();
        }
    }
}
