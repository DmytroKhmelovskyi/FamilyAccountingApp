using FamilyAccounting.BL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyAccounting.BL.Interfaces
{
    public interface ITransactionService
    {
        public Task<TransactionDTO> MakeExpenseAsync(TransactionDTO transaction);
        public Task<TransactionDTO> MakeIncomeAsync(TransactionDTO transaction);
        public Task<TransactionDTO> MakeTransferAsync(TransactionDTO transaction);
        public Task<TransactionDTO> UpdateAsync(int id, TransactionDTO transaction);
        public Task<TransactionDTO> GetAsync(int walletId, int transactionId);
        public Task<TransactionDTO> SetInitialBalanceAsync(TransactionDTO transaction);
        public Task<IEnumerable<CategoryDTO>> GetExpenseCategoriesAsync();
        public Task<IEnumerable<CategoryDTO>> GetIncomeCategoriesAsync();
        public TransactionDTO MakeExpense(TransactionDTO transaction);
        public TransactionDTO MakeIncome(TransactionDTO transaction);
        public TransactionDTO MakeTransfer(TransactionDTO transaction);
        public TransactionDTO Update(int id, TransactionDTO transaction);
        public TransactionDTO Get(int walletId, int transactionId);
        public TransactionDTO SetInitialBalance(TransactionDTO transaction);
        public IEnumerable<CategoryDTO> GetExpenseCategories();
        public IEnumerable<CategoryDTO> GetIncomeCategories();
    }
}
