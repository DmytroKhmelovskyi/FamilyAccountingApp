using System;

namespace FamilyAccounting.Web.Models
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public WalletViewModel SourceWallet { get; set; }
        public WalletViewModel TargetWallet { get; set; }
        public string Description { get; set; }
        public DateTime TimeStamp { get; set; }
        public CategoryViewModel Category { get; set; }
        public bool State { get; set; }
        public TransactionTypeViewModel TransactionType { get; set; }
        public decimal BalanceBefore { get; set; }
        public decimal BalanceAfter { get; set; }
    }
}
