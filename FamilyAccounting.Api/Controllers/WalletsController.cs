using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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
        public ActionResult Index()
        {
            var wallet = walletService.Get();
            return new OkObjectResult(wallet.ToList());
        }

        [HttpPost("{id}")]
        public ActionResult MakeActive(int Id)
        {
            walletService.MakeActive(Id);
            return new OkResult();
        }

        [HttpGet("{id}")]
        public ActionResult Details(int Id)
        {
            var wallet = walletService.Get(Id);
            wallet.Transactions = walletService.GetTransactions(Id).OrderByDescending(x => x.TimeStamp);
            return new OkObjectResult(wallet);
        }

        [HttpGet("{id}")]
        public ActionResult Update(int id)
        {
            var updatedWallet = walletService.Get(id);
            return new OkObjectResult(updatedWallet);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, WalletDTO wallet)
        {
            if (ModelState.IsValid)
            {
                walletService.Update(id, wallet);

                return new OkResult();
            }
            return Content("Invalid inputs");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var wallet = walletService.Get((int)id);
            return new OkObjectResult(wallet);
        }

        [ActionName("Delete")]
        [HttpDelete("{id}")]
        public ActionResult DeleteWallet(int? id)
        {
            var wallet = walletService.Get((int)id);
            walletService.Delete((int)id);
            return new OkResult();
        }

        [HttpGet("{id}")]
        public ActionResult Create(int id)
        {
            var person = personService.Get(id);
            WalletDTO walletVM = new WalletDTO
            {
                PersonId = person.Id
            };
            return new OkObjectResult(walletVM);
        }

        [HttpPost]
        public ActionResult Create(WalletDTO wallet)
        {
            walletService.Create(wallet);
            return new OkResult();
        }
    }
}
