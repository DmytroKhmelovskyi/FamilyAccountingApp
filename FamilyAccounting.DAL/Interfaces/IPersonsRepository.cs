using FamilyAccounting.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyAccounting.DAL.Interfaces
{
    public interface IPersonsRepository
    {
        public Person Add(Person person);
        public Person Update(int id, Person person);
        IEnumerable<Person> GetListOfPersons();
    }
}
