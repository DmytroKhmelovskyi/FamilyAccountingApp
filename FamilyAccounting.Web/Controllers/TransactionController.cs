using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Details(int walletId, int transactionId)
        {
            var transaction = await transactionWebService.Get(walletId, transactionId);
            return View(transaction);
        }

        [HttpGet]
        public async Task<IActionResult> MakeExpense(int id)
        {
            var wallet = await walletWebService.Get(id);
            var categories = await transactionWebService.GetExpenseCategories();
            var transaction = new TransactionViewModel
            {
                SourceWalletId = (int)wallet.Id,
                SourceWallet = wallet.Description
            };
            ViewBag.Categories = categories;
            return View(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> MakeExpense(TransactionViewModel transaction)
        {
            await transactionWebService.MakeExpense(transaction);
            return RedirectToAction("Details", "Wallet", new
            {
                id = transaction.SourceWalletId
            });
        }

        [HttpGet]
        public async Task<IActionResult> MakeIncome(int id)
        {
            var wallet = await walletWebService.Get(id);
            var categories = await transactionWebService.GetIncomeCategories();
            var transaction = new TransactionViewModel
            {
                TargetWalletId = (int)wallet.Id,
                TargetWallet = wallet.Description
            };
            ViewBag.Categories = categories;
            return View(transaction);
        }

        [HttpGet]
        public async Task<IActionResult> MakeTransfer(int id)
        {
            var wallet = await walletWebService.Get(id);
            var transaction = new TransactionViewModel
            {
                SourceWalletId = (int)wallet.Id,
                SourceWallet = wallet.Description,
                 
            };
            var wallets = await walletWebService.Get();
            ViewBag.Wallets = wallets;
            return View(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> MakeIncome(TransactionViewModel transaction)
        {
            await transactionWebService.MakeIncome(transaction);
            return RedirectToAction("Details", "Wallet", new
            {
                id = transaction.TargetWalletId
            });
        }

        [HttpPost]
        public async Task<IActionResult> MakeTransfer(TransactionViewModel transaction)
        {
            await transactionWebService.MakeTransfer(transaction);
            return RedirectToAction("Details", "Wallet", new
            {
                id = transaction.SourceWalletId
            });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id, int transactionId)
        {
            var updatedTransaction = await transactionWebService.Get(id, transactionId);
            return View(updatedTransaction);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, TransactionViewModel transaction)
        {
            if (ModelState.IsValid)
            {
                await transactionWebService.Update(id, transaction);
            }
            return RedirectToAction("Details", "Wallet", new { id =  transaction.TargetWalletId });
        }

        [HttpGet]
        public async Task<IActionResult> SetInitialBalance(int id)
        {
            var wallet = await walletWebService.Get(id);
            var transaction = new TransactionViewModel
            {

                SourceWalletId = (int)wallet.Id,
                SourceWallet = wallet.Description
            };
            return View(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> SetInitialBalance(TransactionViewModel transaction)
        {
            await transactionWebService.SetInitialBalance(transaction);
            return RedirectToAction("Details", "Wallet", new
            {
                id = transaction.SourceWalletId
            });
        }
    }
}
