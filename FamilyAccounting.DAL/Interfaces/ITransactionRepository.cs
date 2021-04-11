using FamilyAccounting.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyAccounting.DAL.Interfaces
{
    public interface ITransactionRepository
    {
        public Task<Transaction> MakeExpense(Transaction transaction);
        public Task<Transaction> MakeIncome(Transaction transaction);
        public Task<Transaction> MakeTransfer(Transaction transaction);
        public Task<Transaction> Update(int id, Transaction transaction);
        public Task<Transaction> Get(int walletId, int transactionId);
        public Task<Transaction> SetInitialBalance(Transaction transaction);
        public Task<IEnumerable<Category>> GetExpenseCategories();
        public Task<IEnumerable<Category>> GetIncomeCategories();
    }
}
