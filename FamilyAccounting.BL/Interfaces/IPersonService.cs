using FamilyAccounting.BL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyAccounting.BL.Interfaces
{
    public interface IPersonService
    {
        public Task<IEnumerable<PersonDTO>> Get();
        public Task<IEnumerable<WalletDTO>> GetWallets(int id);
        public Task<PersonDTO> Add(PersonDTO person);
        public Task<PersonDTO> Update(int id, PersonDTO person);
        public Task<PersonDTO> Get(int id);
        public Task<int> Delete(int id);
    }
}
