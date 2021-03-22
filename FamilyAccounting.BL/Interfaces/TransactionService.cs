using FamilyAccounting.BL.DTO;

namespace FamilyAccounting.BL.Interfaces
{
    public interface ITransactionService
    {
        public TransactionDTO MakeExpense(TransactionDTO transaction);
        public TransactionDTO Update(int id, TransactionDTO transaction);
    }
}
