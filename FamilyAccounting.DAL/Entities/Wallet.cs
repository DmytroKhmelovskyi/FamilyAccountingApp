using System.Collections.Generic;

namespace FamilyAccounting.DAL.Entities
{
    public class Wallet
    {
        public int? Id { get; set; }
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public string Description { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public bool IsCash { get; set; }
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public bool IsActive { get; set; }
    }
}
