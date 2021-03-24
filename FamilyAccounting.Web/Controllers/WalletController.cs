using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Models;
using FamilyAccounting.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FamilyAccounting.Web.Controllers
{
    public class WalletController : Controller
    {
        private readonly IWalletService walletService;
        private readonly IPersonService personService;
        private readonly IMapper mapper;

        public WalletController(IWalletService walletService, IPersonService personService, IMapper mapper)
        {
            this.walletService = walletService;
            this.personService = personService;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            try
            {
                var wallet = walletService.Get();
                var IndexVM = WalletMapper.WalletMap(wallet);
                return View(IndexVM);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IActionResult Details(int Id)
        {
                WalletDTO wallet = walletService.Get(Id);
                wallet.Transactions = walletService.GetTransactions(Id);
                return View(WalletMapper.WalletMap(wallet, wallet.Transactions));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            try
            {
                var updatedWallet = walletService.Get(id);
                return View(WalletMapper.WalletMap(updatedWallet));
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
                walletService.Update(id, WalletMapper.WalletMap(wallet));
            }
            return RedirectToAction("Details", "Wallet", new { id = wallet.Id });
        }
        [HttpGet]
        public ViewResult Delete(int? id)
        {
            var wallet = walletService.Get((int)id);
            return View(WalletMapper.WalletMap(wallet));
        }

        [ActionName("Delete")]
        [HttpPost]
        public IActionResult DeleteWallet(int? id)
        {
            walletService.Delete((int)id);
            return RedirectToAction("Index");
        }

        public IActionResult Create(int id)
        {
            PersonDTO person = personService.Get(id);
            WalletViewModel walletVM = new WalletViewModel
            {
                PersonId = person.Id
            };
            return View(walletVM);
        }

        [HttpPost]
        public IActionResult Create(WalletViewModel wallet)
        {
            var _wallet = WalletMapper.WalletMap(wallet);
            
            walletService.Create(_wallet);
            return RedirectToAction("Details", "Person", new { id = wallet.PersonId});
        }
    }
}
