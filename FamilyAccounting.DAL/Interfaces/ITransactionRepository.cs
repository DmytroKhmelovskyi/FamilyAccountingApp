using FamilyAccounting.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyAccounting.DAL.Interfaces
{
    public interface ITransactionRepository
    {
        public Task<Transaction> MakeExpenseAsync(Transaction transaction);
        public Task<Transaction> MakeIncomeAsync(Transaction transaction);
        public Task<Transaction> MakeTransferAsync(Transaction transaction);
        public Task<Transaction> UpdateAsync(int id, Transaction transaction);
        public Task<Transaction> GetAsync(int walletId, int transactionId);
        public Task<Transaction> SetInitialBalanceAsync(Transaction transaction);
        public Task<IEnumerable<Category>> GetExpenseCategoriesAsync();
        public Task<IEnumerable<Category>> GetIncomeCategoriesAsync();
        public Transaction MakeExpense(Transaction transaction);
        public Transaction MakeIncome(Transaction transaction);
        public Transaction MakeTransfer(Transaction transaction);
        public Transaction Update(int id, Transaction transaction);
        public Transaction Get(int walletId, int transactionId);
        public Transaction SetInitialBalance(Transaction transaction);
        public IEnumerable<Category> GetExpenseCategories();
        public IEnumerable<Category> GetIncomeCategories();
    }
}
