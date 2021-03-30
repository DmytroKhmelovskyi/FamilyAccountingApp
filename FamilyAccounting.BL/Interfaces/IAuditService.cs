using FamilyAccounting.BL.DTO;
using System.Collections.Generic;

namespace FamilyAccounting.BL.Interfaces
{
    public interface IAuditService
    {
        public IEnumerable<AuditActionDTO> GetActions();
        public IEnumerable<AuditWalletDTO> GetWallets();
        public IEnumerable<AuditPersonDTO> GetPersons();
    }
}
