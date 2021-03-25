using FamilyAccounting.BL.DTO;
using System.Collections.Generic;

namespace FamilyAccounting.BL.Interfaces
{
    public interface ITransactionService
    {
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
