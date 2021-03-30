using System;

namespace FamilyAccounting.DAL.Entities
{
    public class AuditWallet
    {
        public Wallet Wallet { get; set; }
        public string Type { get; set; }
        public DateTime Time { get; set; }
    }
}
