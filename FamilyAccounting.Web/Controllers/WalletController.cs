using FamilyAccounting.Web.Exceptions;
using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using FamilyAccounting.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
        public IActionResult Index()
        {
            var wallet = walletWebService.Get();
            return View(wallet);
        }

        public IActionResult MakeActive(int Id)
        {
            walletWebService.MakeActive(Id);
            return RedirectToAction("Details", "Wallet", new { id = Id });
        }
        public IActionResult Details(int Id, int? page)
        {
            var pageNumber = page ?? 1;
            var wallet = walletWebService.Get(Id);
            wallet.Transactions = walletWebService.GetTransactions(Id).OrderByDescending(x => x.TimeStamp);
            var onePageOfTransactions = wallet.Transactions.ToPagedList(pageNumber, 4);
            ViewBag.OnePageOfTransactions = onePageOfTransactions;
            return View(wallet);
        }

        public IActionResult Detail(int Id, DateTime from, DateTime to)
        {
            var wallet = walletWebService.Get(Id);
            wallet.Transactions = walletWebService.GetTransactions(Id, from, to).OrderByDescending(x => x.TimeStamp);
            return View(wallet);
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            //throw new NotFoundException();
            var updatedWallet = walletWebService.Get(id);
            return View(updatedWallet);
        }

        [HttpPost]
        public IActionResult Update(int id, WalletViewModel wallet)
        {
            if (ModelState.IsValid)
            {
                walletWebService.Update(id, wallet);
            }
            return RedirectToAction("Details", "Wallet", new { id = wallet.Id });
        }

        [HttpGet]
        public ViewResult Delete(int? id)
        {
            //throw new BadRequestException();
            var wallet = walletWebService.Get((int)id);
            return View(wallet);
        }

        [ActionName("Delete")]
        [HttpPost]
        public IActionResult DeleteWallet(int? id)
        {
            var wallet = walletWebService.Get((int)id);
            walletWebService.Delete((int)id);
            return RedirectToAction("Details", "Person", new { id = wallet.PersonId });
        }

        public IActionResult Create(int id)
        {
            var person = personWebService.Get(id);
            WalletViewModel walletVM = new WalletViewModel
            {
                PersonId = person.Id
            };
            return View(walletVM);
        }

        [HttpPost]
        public IActionResult Create(WalletViewModel wallet)
        {
            walletWebService.Create(wallet);
            return RedirectToAction("Details", "Person", new
            {
                id = wallet.PersonId
            });
        }
    }
}
