using FamilyAccounting.Web.Models;

namespace FamilyAccounting.Web.Interfaces
{
    public interface ITransactionWebService
    {
        public TransactionViewModel MakeExpense(TransactionViewModel transaction);
        public TransactionViewModel MakeIncome(TransactionViewModel transaction);
        public TransactionViewModel MakeTransfer(TransactionViewModel transaction);
        public TransactionViewModel Update(int id, TransactionViewModel transaction);
        public TransactionViewModel Get(int walletId, int transactionId);
        public TransactionViewModel SetInitialBalance(TransactionViewModel transaction);
    }
}
