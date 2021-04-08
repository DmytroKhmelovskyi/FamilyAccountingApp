using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FamilyAccounting.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService transactionService;
        private readonly IWalletService walletService;

        public TransactionsController(ITransactionService transactionWebService, IWalletService walletWebService)
        {
            this.transactionService = transactionWebService;
            this.walletService = walletWebService;
        }

        [HttpGet("{id}")]
        public ActionResult<TransactionDTO> Details(int walletId, int transactionId)
        {
            var transaction = transactionService.Get(walletId, transactionId);
            return transaction;
        }

        [HttpGet("{id}")]
        public ActionResult<TransactionDTO> MakeExpense(int id)
        {
            var wallet = walletService.Get(id);
            var categories = transactionService.GetExpenseCategories();
            var transaction = new TransactionDTO
            {
                SourceWalletId = (int)wallet.Id,
                SourceWallet = wallet.Description
            };
            return transaction;
        }

        [HttpPost]
        public IActionResult MakeExpense(TransactionDTO transaction)
        {
            transactionService.MakeExpense(transaction);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<TransactionDTO> MakeIncome(int id)
        {
            var wallet = walletService.Get(id);
            var categories = transactionService.GetIncomeCategories();
            var transaction = new TransactionDTO
            {
                TargetWalletId = (int)wallet.Id,
                TargetWallet = wallet.Description
            };
            return transaction;
        }

        [HttpPost]
        public ActionResult MakeIncome(TransactionDTO transaction)
        {
            transactionService.MakeIncome(transaction);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<TransactionDTO> MakeTransfer(int id)
        {
            var wallet = walletService.Get(id);
            var transaction = new TransactionDTO
            {
                SourceWalletId = (int)wallet.Id,
                SourceWallet = wallet.Description,

            };
            var wallets = walletService.Get();
            return transaction;
        }

        [HttpPost]
        public ActionResult MakeTransfer(TransactionDTO transaction)
        {
            transactionService.MakeTransfer(transaction);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<TransactionDTO> Update(int id, int transactionId)
        {
            var updatedTransaction = transactionService.Get(id, transactionId);
            return updatedTransaction;
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, TransactionDTO transaction)
        {
            if (ModelState.IsValid)
            {
                transactionService.Update(id, transaction);
                return Ok();
            }
            return Content("Invalid inputs");
        }

        [HttpGet]
        public ActionResult<TransactionDTO> SetInitialBalance(int id)
        {
            var wallet = walletService.Get(id);
            var transaction = new TransactionDTO
            {

                SourceWalletId = (int)wallet.Id,
                SourceWallet = wallet.Description
            };
            return transaction;
        }

        [HttpPost]
        public ActionResult SetInitialBalance(TransactionDTO transaction)
        {
            transactionService.SetInitialBalance(transaction);
            return Ok();
        }
    }
}
