using FamilyAccounting.BL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyAccounting.BL.Interfaces
{
    public interface IAuditService
    {
        public IEnumerable<AuditActionDTO> GetActions();
        public IEnumerable<AuditWalletDTO> GetWallets();
        public IEnumerable<AuditPersonDTO> GetPersons();
        public Task<IEnumerable<AuditActionDTO>> GetActionsAsync();
        public Task<IEnumerable<AuditWalletDTO>> GetWalletsAsync();
        public Task<IEnumerable<AuditPersonDTO>> GetPersonsAsync();
    }
}
