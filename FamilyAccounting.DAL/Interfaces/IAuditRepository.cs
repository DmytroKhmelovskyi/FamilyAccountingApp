using FamilyAccounting.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyAccounting.DAL.Interfaces
{
    public interface IAuditRepository
    {
        public IEnumerable<AuditAction> GetActions();
        public IEnumerable<AuditWallet> GetWallets();
        public IEnumerable<AuditPerson> GetPersons();
        public Task<IEnumerable<AuditAction>> GetActionsAsync();
        public Task<IEnumerable<AuditWallet>> GetWalletsAsync();
        public Task<IEnumerable<AuditPerson>> GetPersonsAsync();

    }
}
