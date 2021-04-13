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
            return new OkObjectResult(await walletService.Get());
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> MakeActive(int Id)
        {
            await walletService.MakeActive(Id);
            return new OkResult();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Details(int Id)
        {
            var wallet = await walletService.Get(Id);
            wallet.Transactions = await walletService.GetTransactions(Id);
            return new OkObjectResult(wallet);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Update(int id)
        {
            return new OkObjectResult(await walletService.Get(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, WalletDTO wallet)
        {
            if (ModelState.IsValid)
            {
                await walletService.Update(id, wallet);
                return new OkResult();
            }
            return Content("Invalid inputs");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            return new OkObjectResult(await walletService.Get((int)id));
        }

        [ActionName("Delete")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWallet(int? id)
        {
            var wallet = walletService.Get((int)id);
            await walletService.Delete((int)id);
            return new OkResult();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Create(int id)
        {
            var person = await personService.Get(id);
            WalletDTO walletVM = new WalletDTO
            {
                PersonId = person.Id
            };
            return new OkObjectResult(walletVM);
        }

        [HttpPost]
        public async Task<ActionResult> Create(WalletDTO wallet)
        {
            await walletService.Create(wallet);
            return new OkResult();
        }
    }
}
