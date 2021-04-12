using FamilyAccounting.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyAccounting.DAL.Interfaces
{
    public interface IPersonRepository
    {
        public Task<Person> Add(Person person);
        public Task<Person> Update(int id, Person person);
        public  Task<IEnumerable<Person>> Get();
        public Task<IEnumerable<Wallet>> GetWallets(int id);
        public Task<Person> Get(int id);
        public Task<int> Delete(int id);
    }
}
