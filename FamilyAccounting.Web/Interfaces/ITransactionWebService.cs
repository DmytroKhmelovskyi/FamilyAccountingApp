using FamilyAccounting.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyAccounting.Web.Interfaces
{
    public interface ITransactionWebService
    {
        public Task<TransactionViewModel> MakeExpense(TransactionViewModel transaction);
        public Task<TransactionViewModel> MakeIncome(TransactionViewModel transaction);
        public Task<TransactionViewModel> MakeTransfer(TransactionViewModel transaction);
        public Task<TransactionViewModel> Update(int id, TransactionViewModel transaction);
        public Task<TransactionViewModel> Get(int walletId, int transactionId);
        public Task<TransactionViewModel> SetInitialBalance(TransactionViewModel transaction);
        public Task<IEnumerable<CategoryViewModel>> GetExpenseCategories();
        public Task<IEnumerable<CategoryViewModel>> GetIncomeCategories();
    }
}
