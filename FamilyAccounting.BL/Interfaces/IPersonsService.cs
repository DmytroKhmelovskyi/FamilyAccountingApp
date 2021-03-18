using FamilyAccounting.BL.DTO;
using System.Collections.Generic;

namespace FamilyAccounting.BL.Interfaces
{
    interface IPersonsService
    {
        IEnumerable<PersonDTO> GetListOfPersons();
    }
}
