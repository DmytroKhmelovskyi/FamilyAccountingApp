using System.Collections.Generic;

namespace FamilyAccounting.DAL.Entities
{
    public class Wallet
    {
        public int Id { get; set; }
        public Person Person { get; set; }
        public string Description { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public bool CashType { get; set; }
        public Card Card { get; set; }
        public decimal Income { get; set; }
        public decimal Expense { get; set; }

    }
}
