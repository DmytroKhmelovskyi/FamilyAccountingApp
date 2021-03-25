using FamilyAccounting.DAL.Entities;

namespace FamilyAccounting.DAL.Interfaces
{
    public interface ITransactionRepository
    {
        public Transaction MakeExpense(Transaction transaction);
        public Transaction MakeIncome(Transaction transaction);
        public Transaction MakeTransfer(Transaction transaction);
        public Transaction Update(int id, Transaction transaction);
        public Transaction Get(int walletId, int transactionId);
        public Transaction SetInitialBalance(Transaction transaction);
    }
}
