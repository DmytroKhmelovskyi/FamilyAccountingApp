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
        public async Task<IEnumerable<AuditActionDTO>> GetActionsAsync()
        {
            return await Task.Run(() => GetActions());
        }
        public async Task<IEnumerable<AuditWalletDTO>> GetWalletsAsync()
        {
            return await Task.Run(() => GetWallets());
        }
        public async Task<IEnumerable<AuditPersonDTO>> GetPersonsAsync()
        {
            return await Task.Run(() => GetPersons());
        }
    }
}
