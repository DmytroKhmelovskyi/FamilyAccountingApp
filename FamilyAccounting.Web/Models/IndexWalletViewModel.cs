using System.Collections.Generic;

namespace FamilyAccounting.Web.Models
{
    public class IndexWalletViewModel
    {
        public IEnumerable<WalletViewModel> Wallets { get; set; }
    }
}
