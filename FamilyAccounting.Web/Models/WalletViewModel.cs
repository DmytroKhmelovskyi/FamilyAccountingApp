using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FamilyAccounting.Web.Models
{
    public class WalletViewModel
    {
        public int Id { get; set; }
        public PersonViewModel Person { get; set; }
        [Required]
        public string Description { get; set; }
        public decimal Balance { get; set; }
        //public IEnumerable<Transaction> Transactions { get; set; }
        public bool IsCash { get; set; }
        //public Card Card { get; set; }
        public IEnumerable<TransactionViewModel> Transactions { get; set; }
        public CardViewModel Card { get; set; }
        public decimal Income { get; set; }
        public decimal Expense { get; set; }
        public bool IsActive { get; set; }
    }
}
