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
        public ActionResult<IEnumerable<WalletDTO>> Index()
        {
            var wallet = walletService.Get();
            return wallet.ToList();
        }

        [HttpPost("{id}")]
        public ActionResult MakeActive(int Id)
        {
            walletService.MakeActive(Id);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<WalletDTO> Details(int Id)
        {
            var wallet = walletService.Get(Id);
            wallet.Transactions = walletService.GetTransactions(Id).OrderByDescending(x => x.TimeStamp);
            return wallet;
        }

        [HttpGet("{id}")]
        public ActionResult<WalletDTO> Update(int id)
        {
            var updatedWallet = walletService.Get(id);
            return updatedWallet;
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, WalletDTO wallet)
        {
            if (ModelState.IsValid)
            {
                walletService.Update(id, wallet);

                return Ok();
            }
            return Content("Invalid inputs");
        }

        //[HttpGet]
        //public ViewResult Delete(int? id)
        //{
        //    //throw new BadRequestException();
        //    var wallet = walletService.Get((int)id);
        //    return View(wallet);
        //}

        [ActionName("Delete")]
        [HttpDelete("{id}")]
        public ActionResult DeleteWallet(int? id)
        {
            var wallet = walletService.Get((int)id);
            walletService.Delete((int)id);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<WalletDTO> Create(int id)
        {
            var person = personService.Get(id);
            WalletDTO walletVM = new WalletDTO
            {
                PersonId = person.Id
            };
            return walletVM;
        }

        [HttpPost]
        public ActionResult Create(WalletDTO wallet)
        {
            walletService.Create(wallet);
            return Ok();
        }
    }
}
