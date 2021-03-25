using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using FamilyAccounting.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
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
                return BadRequest();
            }
        }

        public IActionResult Details(int Id, int? page)
        {
            var pageNumber = page ?? 1;
            var wallet = walletWebService.Get(Id);
            wallet.Transactions = walletWebService.GetTransactions(Id);
            var onePageOfTransactions = wallet.Transactions.ToPagedList(pageNumber, 4);
            ViewBag.OnePageOfTransactions = onePageOfTransactions;
            return View(wallet);
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
                return BadRequest();
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
            var wallet = walletWebService.Get((int)id);
            return View(wallet);
        }

        [ActionName("Delete")]
        [HttpPost]
        public IActionResult DeleteWallet(int? id)
        {
            walletWebService.Delete((int)id);
            return RedirectToAction("Index");
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
            return RedirectToAction("Details", "Person", new { id = wallet.PersonId});
        }
    }
}
