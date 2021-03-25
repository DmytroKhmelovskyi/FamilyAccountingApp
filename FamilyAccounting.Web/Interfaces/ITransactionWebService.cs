using FamilyAccounting.Web.Models;

namespace FamilyAccounting.Web.Interfaces
{
    public interface ITransactionWebService
    {
        public TransactionViewModel MakeExpense(TransactionViewModel transaction);
        public TransactionViewModel MakeTransfer(TransactionViewModel transaction);
        public TransactionViewModel Update(int id, TransactionViewModel transaction);
        public TransactionViewModel Get(int id);
        public TransactionViewModel SetInitialBalance(TransactionViewModel transaction);
    }
}
