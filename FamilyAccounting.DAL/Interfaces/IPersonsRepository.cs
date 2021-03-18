using FamilyAccounting.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyAccounting.DAL.Interfaces
{
    public interface IPersonsRepository
    {
        IEnumerable<Person> GetListOfPersons();
    }
}
