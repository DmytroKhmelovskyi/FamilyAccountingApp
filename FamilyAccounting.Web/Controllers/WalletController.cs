using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
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
            try
            {
                var wallet = walletWebService.Get();
                return View(wallet);
            }
            catch (Exception)
            {
                throw new Exception("Exception");
            }
        }

        public IActionResult Details(int Id, int? page)
        {           
            try
            {
                var pageNumber = page ?? 1;
                var wallet = walletWebService.Get(Id);
                wallet.Transactions = walletWebService.GetTransactions(Id).OrderByDescending(x => x.TimeStamp);
                var onePageOfTransactions = wallet.Transactions.ToPagedList(pageNumber, 4);
                ViewBag.OnePageOfTransactions = onePageOfTransactions;
                return View(wallet);
            }
            catch (Exception)
            {
                throw new Exception("Exception");
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            try
            {
                var updatedWallet = walletWebService.Get(id);
                return View(updatedWallet);
            }
            catch (Exception)
            {
                throw new Exception("Exception");
            }
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
            try
            {
                var wallet = walletWebService.Get((int)id);
                return View(wallet);
            }
            catch (Exception)
            {
                throw new Exception("Exception");
            }
        }

        [ActionName("Delete")]
        [HttpPost]
        public IActionResult DeleteWallet(int? id )
        {
            try
            {
             var wallet = walletWebService.Get((int)id);
            walletWebService.Delete((int)id);
            return RedirectToAction("Details","Person", new { id = wallet.PersonId  });
            }
            catch(Exception)
            {
                throw new Exception("Exception");
            }
        }

        public IActionResult Create(int id)
        {
            try
            {
                var person = personWebService.Get(id);
                WalletViewModel walletVM = new WalletViewModel
                {
                    PersonId = person.Id
                };
                return View(walletVM);
            }
            catch(Exception)
            {
                throw new Exception("Exception");
            }
        }

        [HttpPost]
        public IActionResult Create(WalletViewModel wallet)
        {
            try
            {
                walletWebService.Create(wallet);
                return RedirectToAction("Details", "Person", new
                {
                    id = wallet.PersonId
                });
            }
            catch (Exception) 
            { 
                throw new Exception("Exception"); 
            }
        }
    }
}
