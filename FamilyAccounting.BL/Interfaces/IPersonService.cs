using FamilyAccounting.BL.DTO;
using FamilyAccounting.DAL.Entities;
using System.Collections.Generic;

namespace FamilyAccounting.BL.Interfaces
{
    public interface IPersonService
    {
        public IEnumerable<PersonDTO> Get();
        IEnumerable<WalletDTO> GetWallets(int id);
        public PersonDTO Add(PersonDTO person);
        public PersonDTO Update(int id, PersonDTO person);
        public PersonDTO Get(int id);
        public void Delete(int id);
    }
}
