using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<ActionResult<TransactionDTO>> Details(int walletId, int transactionId)
        {
            var transaction = await transactionService.Get(walletId, transactionId);
            return transaction;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDTO>> MakeExpense(int id)
        {
            var wallet = await walletService.Get(id);
            var categories = await transactionService.GetExpenseCategories();
            var transaction = new TransactionDTO
            {
                SourceWalletId = (int)wallet.Id,
                SourceWallet = wallet.Description
            };
            return transaction;
        }

        [HttpPost]
        public async Task<IActionResult> MakeExpense(TransactionDTO transaction)
        {
            await transactionService.MakeExpense(transaction);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDTO>> MakeIncome(int id)
        {
            var wallet = await walletService.Get(id);
            var categories = await transactionService.GetIncomeCategories();
            var transaction = new TransactionDTO
            {
                TargetWalletId = (int)wallet.Id,
                TargetWallet = wallet.Description
            };
            return transaction;
        }

        [HttpPost]
        public async Task<ActionResult> MakeIncome(TransactionDTO transaction)
        {
            await transactionService.MakeIncome(transaction);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDTO>> MakeTransfer(int id)
        {
            var wallet = await walletService.Get(id);
            var transaction = new TransactionDTO
            {
                SourceWalletId = (int)wallet.Id,
                SourceWallet = wallet.Description,

            };
            var wallets = await walletService.Get();
            return transaction;
        }

        [HttpPost]
        public async Task<ActionResult> MakeTransfer(TransactionDTO transaction)
        {
            await transactionService.MakeTransfer(transaction);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDTO>> Update(int id, int transactionId)
        {
            var updatedTransaction = await transactionService.Get(id, transactionId);
            return updatedTransaction;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, TransactionDTO transaction)
        {
            if (ModelState.IsValid)
            {
                await transactionService.Update(id, transaction);
                return Ok();
            }
            return Content("Invalid inputs");
        }

        [HttpGet]
        public async Task<ActionResult<TransactionDTO>> SetInitialBalance(int id)
        {
            var wallet = await walletService.Get(id);
            var transaction = new TransactionDTO
            {

                SourceWalletId = (int)wallet.Id,
                SourceWallet = wallet.Description
            };
            return transaction;
        }

        [HttpPost]
        public async Task<ActionResult> SetInitialBalance(TransactionDTO transaction)
        {
            await transactionService.SetInitialBalance(transaction);
            return Ok();
        }
    }
}
