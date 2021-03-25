using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.BL.Services;
using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using FamilyAccounting.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FamilyAccounting.Web.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionWebService transactionWebService;
        private readonly IWalletWebService walletWebService;

        public TransactionController(ITransactionWebService transactionWebService, IWalletWebService walletWebService)
        {
            this.transactionWebService = transactionWebService;
            this.walletWebService = walletWebService;
        }

        [HttpGet]
        public IActionResult MakeExpense(int id)
        {
            try
            {
                var wallet = walletWebService.Get(id);
                var transaction = new TransactionViewModel
                {
                    SourceWalletId = (int)wallet.Id,
                    SourceWallet = wallet.Description
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
            transactionWebService.MakeExpense(transaction);
            return RedirectToAction("Details", "Wallet", new { id = transaction.SourceWalletId });
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            try
            {
                var updatedTransaction = transactionWebService.Get(id);
                return View(updatedTransaction);
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
                transactionWebService.Update(id, transaction);
            }
            return RedirectToAction("Details", "Wallet", new { id = transaction.Id });
        }
    }
}
