using System;

namespace FamilyAccounting.Web.Models
{
    public class AuditWalletViewModel
    {
        public WalletViewModel Wallet { get; set; }
        public string Type { get; set; }
        public DateTime Time { get; set; }
    }
}
