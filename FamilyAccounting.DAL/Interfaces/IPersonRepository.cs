using FamilyAccounting.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyAccounting.DAL.Interfaces
{
    public interface IPersonRepository
    {
        public Person Add(Person person);
        public Person Update(int id, Person person);
        public IEnumerable<Person> Get();
        public IEnumerable<Wallet> GetWallets(int id);
        public Person Get(int id);
        public int Delete(int id);
    }
}
