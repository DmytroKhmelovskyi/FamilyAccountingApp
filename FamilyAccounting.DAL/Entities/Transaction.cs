namespace FamilyAccounting.DAL.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public Wallet SourceWallet { get; set; }
        public Wallet TargetWallet { get; set; }
        public Person Person { get; set; }
        public Category Category { get; set; }
        public bool State { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal BalanceBefore { get; set; }
        public decimal BalanceAfter { get; set; }
    }
}
