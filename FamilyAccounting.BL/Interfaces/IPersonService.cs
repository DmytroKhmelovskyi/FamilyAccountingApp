using FamilyAccounting.BL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyAccounting.BL.Interfaces
{
    public interface IPersonService
    {
        public Task<IEnumerable<PersonDTO>> GetAsync();
        public Task<IEnumerable<WalletDTO>> GetWalletsAsync(int id);
        public Task<PersonDTO> AddAsync(PersonDTO person);
        public Task<PersonDTO> UpdateAsync(int id, PersonDTO person);
        public Task<PersonDTO> GetAsync(int id);
        public Task<int> DeleteAsync(int id);
        public IEnumerable<PersonDTO> Get();
        public IEnumerable<WalletDTO> GetWallets(int id);
        public PersonDTO Add(PersonDTO person);
        public PersonDTO Update(int id, PersonDTO person);
        public PersonDTO Get(int id);
        public int Delete(int id);
    }
}
