using System;

namespace FamilyAccounting.DAL.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int SourceWalletId { get; set; }
        public string SourceWallet { get; set; }
        public int TargetWalletId { get; set; }
        public string TargetWallet { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool State { get; set; }
        public TransactionType TransactionType { get; set; }
        //public int TransactionType { get; set; }
        public decimal BalanceBefore { get; set; }
        public decimal BalanceAfter { get; set; }
    }
}
