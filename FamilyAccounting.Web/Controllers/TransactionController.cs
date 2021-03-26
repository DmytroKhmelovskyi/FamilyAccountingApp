using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
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
        public IActionResult Details(int walletId, int transactionId)
        {
            try
            {
                var transaction = transactionWebService.Get(walletId, transactionId);
                return View(transaction);
            }
            catch(Exception)
            {
                throw new Exception("Exception");
            }
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
                throw new Exception("Exception");
            }
        }

        [HttpPost]
        public IActionResult MakeExpense(TransactionViewModel transaction)
        {
            try
            {
                transactionWebService.MakeExpense(transaction);
                return RedirectToAction("Details", "Wallet", new
                {
                    id = transaction.SourceWalletId
                });
            }
            catch (Exception) 
            {
                throw new Exception("Exception"); 
            }
        }

        [HttpGet]
        public IActionResult MakeIncome(int id)
        {
            try
            {
                var wallet = walletWebService.Get(id);
                var transaction = new TransactionViewModel
                {
                    TargetWalletId = (int)wallet.Id,
                    TargetWallet = wallet.Description
                };
                return View(transaction);
            }
            catch (Exception)
            {
                throw new Exception("Exception");
            }
        }

        [HttpGet]
        public IActionResult MakeTransfer(int id)
        {
            try
            {
                var wallet = walletWebService.Get(id);
                var transaction = new TransactionViewModel
                {
                    SourceWalletId = (int)wallet.Id,
                    SourceWallet = wallet.Description,
                };
                return View(transaction);
            }
            catch (Exception)
            {
                throw new Exception("Exception");
            }
        }

        [HttpPost]
        public IActionResult MakeIncome(TransactionViewModel transaction)
        {
            try
            {
                transactionWebService.MakeIncome(transaction);
                return RedirectToAction("Details", "Wallet", new
                {
                    id = transaction.TargetWalletId
                });
            }
            catch (Exception) 
            { 
                throw new Exception("Exception"); 
            }
        }
        
        [HttpPost]
        public IActionResult MakeTransfer(TransactionViewModel transaction)
        {
            try
            {
                transactionWebService.MakeTransfer(transaction);
                return RedirectToAction("Details", "Wallet", new
                {
                    id = transaction.SourceWalletId
                });
            }
            catch (Exception) 
            { 
                throw new Exception("Exception"); 
            }
        }

        [HttpGet]
        public IActionResult Update(int id, int transactionId)
        {
            try
            {
                var updatedTransaction = transactionWebService.Get(id, transactionId);
                return View(updatedTransaction);
            }
            catch (Exception)
            {
                throw new Exception("Exception");
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

        [HttpGet]
        public IActionResult SetInitialBalance(int id)
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
                throw new Exception("Exception");
            }
        }

        [HttpPost]
        public IActionResult SetInitialBalance(TransactionViewModel transaction)
        {
            try
            {
                transactionWebService.SetInitialBalance(transaction);
                return RedirectToAction("Details", "Wallet", new
                {
                    id = transaction.SourceWalletId
                });
            }
            catch (Exception) 
            { 
                throw new Exception("Exception"); 
            }
        }
    }
}
