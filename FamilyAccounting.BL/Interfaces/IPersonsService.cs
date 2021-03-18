using FamilyAccounting.BL.DTO;
using System.Collections.Generic;

namespace FamilyAccounting.BL.Interfaces
{
    public interface IPersonsService
    {
        public IEnumerable<PersonDTO> Get();
        public PersonDTO Add(PersonDTO person);
        public PersonDTO Update(int id, PersonDTO person);
    }
}
