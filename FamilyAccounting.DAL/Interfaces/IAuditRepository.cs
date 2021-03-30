using FamilyAccounting.DAL.Entities;
using System.Collections.Generic;

namespace FamilyAccounting.DAL.Interfaces
{
    public interface IAuditRepository
    {
        public IEnumerable<AuditAction> GetActions();
        public IEnumerable<AuditWallet> GetWallets();
        public IEnumerable<AuditPerson> GetPersons();
    }
}
