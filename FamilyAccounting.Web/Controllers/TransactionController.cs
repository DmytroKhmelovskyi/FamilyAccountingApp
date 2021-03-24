using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.BL.Services;
using FamilyAccounting.Web.Models;
using FamilyAccounting.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FamilyAccounting.Web.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService transactionService;
        private readonly IWalletService walletService;

        public TransactionController(ITransactionService transactionService,IWalletService walletService)
        {
            this.transactionService = transactionService;
            this.walletService = walletService;
        }

        [HttpGet]
        public IActionResult MakeExpense(int id)
        {
            try
            {
                var wallet = walletService.Get(id);
                var walletVM = MapperService.WalletMap(wallet);
                var transaction = new TransactionViewModel
                {
                    //SourceWallet = walletVM
                };
                return View(transaction);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult MakeExpense(TransactionViewModel transaction)
        {

            //transactionService.MakeExpense(MapperService.TransactionMap(transaction, transaction.SourceWallet, transaction.Category));
            return RedirectToAction("Details", "Wallet", new { id = transaction.SourceWalletId });         
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            try
            {
                var updatedTransaction = transactionService.Get(id);
                return View(MapperService.TransactionMap(updatedTransaction));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Update(int id, TransactionViewModel transaction)
        {
            if (ModelState.IsValid)
            {
                transactionService.Update(id, MapperService.TransactionMap(transaction));
            }
            return RedirectToAction("Details", "Wallet", new { id = transaction.Id });
        }
    }
}
