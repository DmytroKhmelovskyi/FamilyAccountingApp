using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyAccounting.BL.DTO
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public WalletDTO SourceWallet { get; set; }
        public WalletDTO TargetWallet { get; set; }
        public PersonDTO Person { get; set; }
        public CategoryDTO Category { get; set; }
        public bool State { get; set; }
        public TransactionTypeDTO TransactionType { get; set; }
        public decimal BalanceBefore { get; set; }
        public decimal BalanceAfter { get; set; }
    }
}
