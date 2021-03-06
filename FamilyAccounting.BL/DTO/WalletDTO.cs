using System.Collections.Generic;

namespace FamilyAccounting.BL.DTO
{
    public class WalletDTO
    {
        public int? Id { get; set; }
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public string Description { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<TransactionDTO> Transactions { get; set; }
        public bool IsCash { get; set; }
        //public CardDTO Card { get; set; }
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public bool IsActive { get; set; }
    }
}
