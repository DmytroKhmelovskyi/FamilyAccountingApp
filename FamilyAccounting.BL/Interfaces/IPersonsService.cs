using FamilyAccounting.BL.DTO;
using System.Collections.Generic;

namespace FamilyAccounting.BL.Interfaces
{
    public interface IPersonsService
    {
        public IEnumerable<PersonDTO> GetListOfPersons();
        public PersonDTO Add(PersonDTO);
    }
}
