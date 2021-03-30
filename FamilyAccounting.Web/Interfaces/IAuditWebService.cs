using FamilyAccounting.Web.Models;
using System.Collections.Generic;

namespace FamilyAccounting.Web.Interfaces
{
    public interface IAuditWebService
    {
        public IEnumerable<AuditActionViewModel> GetActions();
        public IEnumerable<AuditWalletViewModel> GetWallets();
        public IEnumerable<AuditPersonViewModel> GetPersons();
    }
}
