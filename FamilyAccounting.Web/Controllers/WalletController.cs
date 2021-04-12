using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace FamilyAccounting.Web.Controllers
{
    public class WalletController : Controller
    {
        private readonly IWalletWebService walletWebService;
        private readonly IPersonWebService personWebService;

        public WalletController(IWalletWebService walletWebService, IPersonWebService personWebService)
        {
            this.walletWebService = walletWebService;
            this.personWebService = personWebService;
        }
        public async Task<IActionResult> Index()
        {
            var wallet = await walletWebService.Get();
            return View(wallet);
        }

        public async Task<IActionResult> MakeActive(int Id)
        {
            await walletWebService.MakeActive(Id);
            return RedirectToAction("Details", "Wallet", new { id = Id });
        }
        public async Task<IActionResult> Details(int Id, int? page)
        {
            var pageNumber = page ?? 1;
            var wallet = await walletWebService.Get(Id);
            //wallet.Transactions = walletWebService.GetTransactions(Id).OrderByDescending(x => x.TimeStamp);
            wallet.Transactions = await walletWebService.GetTransactions(Id);
            var onePageOfTransactions = wallet.Transactions.ToPagedList(pageNumber, 4);
            ViewBag.OnePageOfTransactions = onePageOfTransactions;
            return View(wallet);
        }

        public async Task<IActionResult> Detail(int Id, DateTime from, DateTime to)
        {
            var wallet = await walletWebService.Get(Id);
            //wallet.Transactions = walletWebService.GetTransactions(Id, from, to).OrderByDescending(x => x.TimeStamp);\
            wallet.Transactions = await walletWebService.GetTransactions(Id, from, to);
            return View(wallet);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            //throw new NotFoundException();
            var updatedWallet = await walletWebService.Get(id);
            return View(updatedWallet);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, WalletViewModel wallet)
        {
            if (ModelState.IsValid)
            {
                await walletWebService.Update(id, wallet);
            }
            return RedirectToAction("Details", "Wallet", new { id = wallet.Id });
        }

        [HttpGet]
        public async Task<ViewResult> Delete(int? id)
        {
            //throw new BadRequestException();
            var wallet = await walletWebService.Get((int)id);
            return View(wallet);
        }

        [ActionName("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteWallet(int? id)
        {
            var wallet = await walletWebService.Get((int)id);
            await walletWebService.Delete((int)id);
            return RedirectToAction("Details", "Person", new { id = wallet.PersonId });
        }

        //public async Task<IActionResult> Create(int id)
        //{
        //    var person = await personWebService.Get(id);
        //    WalletViewModel walletVM = new WalletViewModel
        //    {
        //        PersonId = person.Id
        //    };
        //    return View(walletVM);
        //}

        [HttpPost]
        public async Task<IActionResult> Create(WalletViewModel wallet)
        {
            await walletWebService.Create(wallet);
            return RedirectToAction("Details", "Person", new
            {
                id = wallet.PersonId
            });
        }
    }
}
