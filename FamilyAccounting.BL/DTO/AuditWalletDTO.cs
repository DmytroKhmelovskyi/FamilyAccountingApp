using System;

namespace FamilyAccounting.BL.DTO
{
    public class AuditWalletDTO
    {
        public WalletDTO Wallet { get; set; }
        public string Type { get; set; }
        public DateTime Time { get; set; }
    }
}
