using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using Microsoft.AspNetCore.Mvc;

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
            var transaction = transactionWebService.Get(walletId, transactionId);
            return View(transaction);
        }

        [HttpGet]
        public IActionResult MakeExpense(int id)
        {
            var wallet = walletWebService.Get(id);
            var transaction = new TransactionViewModel
            {
                SourceWalletId = (int)wallet.Id,
                SourceWallet = wallet.Description
            };
            return View(transaction);
        }

        [HttpPost]
        public IActionResult MakeExpense(TransactionViewModel transaction)
        {
            transactionWebService.MakeExpense(transaction);
            return RedirectToAction("Details", "Wallet", new
            {
                id = transaction.SourceWalletId
            });
        }

        [HttpGet]
        public IActionResult MakeIncome(int id)
        {
            var wallet = walletWebService.Get(id);
            var transaction = new TransactionViewModel
            {
                TargetWalletId = (int)wallet.Id,
                TargetWallet = wallet.Description
            };
            return View(transaction);
        }

        [HttpGet]
        public IActionResult MakeTransfer(int id)
        {
            var wallet = walletWebService.Get(id);
            var transaction = new TransactionViewModel
            {
                SourceWalletId = (int)wallet.Id,
                SourceWallet = wallet.Description,
            };
            return View(transaction);
        }

        [HttpPost]
        public IActionResult MakeIncome(TransactionViewModel transaction)
        {
            transactionWebService.MakeIncome(transaction);
            return RedirectToAction("Details", "Wallet", new
            {
                id = transaction.TargetWalletId
            });
        }
        
        [HttpPost]
        public IActionResult MakeTransfer(TransactionViewModel transaction)
        {
            transactionWebService.MakeTransfer(transaction);
            return RedirectToAction("Details", "Wallet", new
            {
                id = transaction.SourceWalletId
            });
        }

        [HttpGet]
        public IActionResult Update(int id, int transactionId)
        {
            var updatedTransaction = transactionWebService.Get(id, transactionId);
            return View(updatedTransaction);
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
            var wallet = walletWebService.Get(id);
            var transaction = new TransactionViewModel
            {
                SourceWalletId = (int)wallet.Id,
                SourceWallet = wallet.Description
            };
            return View(transaction);
        }

        [HttpPost]
        public IActionResult SetInitialBalance(TransactionViewModel transaction)
        {
            transactionWebService.SetInitialBalance(transaction);
            return RedirectToAction("Details", "Wallet", new
            {
                id = transaction.SourceWalletId
            });
        }
    }
}
