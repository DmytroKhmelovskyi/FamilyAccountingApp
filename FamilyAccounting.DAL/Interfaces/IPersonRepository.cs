using FamilyAccounting.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyAccounting.DAL.Interfaces
{
    public interface IPersonRepository
    {
        public Task<Person> AddAsync(Person person);
        public Task<Person> UpdateAsync(int id, Person person);
        public  Task<IEnumerable<Person>> GetAsync();
        public Task<IEnumerable<Wallet>> GetWalletsAsync(int id);
        public Task<Person> GetAsync(int id);
        public Task<int> DeleteAsync(int id);
        public Person Add(Person person);
        public Person Update(int id, Person person);
        public IEnumerable<Person> Get();
        public IEnumerable<Wallet> GetWallets(int id);
        public Person Get(int id);
        public int Delete(int id);
    }
}
