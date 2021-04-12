using FamilyAccounting.BL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyAccounting.BL.Interfaces
{
    public interface ITransactionService
    {
        public Task<TransactionDTO> MakeExpense(TransactionDTO transaction);
        public Task<TransactionDTO> MakeIncome(TransactionDTO transaction);
        public Task<TransactionDTO> MakeTransfer(TransactionDTO transaction);
        public Task<TransactionDTO> Update(int id, TransactionDTO transaction);
        public Task<TransactionDTO> Get(int walletId, int transactionId);
        public Task<TransactionDTO> SetInitialBalance(TransactionDTO transaction);
        public Task<IEnumerable<CategoryDTO>> GetExpenseCategories();
        public Task<IEnumerable<CategoryDTO>> GetIncomeCategories();
    }
}
