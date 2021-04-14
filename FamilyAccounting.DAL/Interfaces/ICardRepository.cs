using FamilyAccounting.DAL.Entities;
using System.Threading.Tasks;

namespace FamilyAccounting.DAL.Interfaces
{
    public interface ICardRepository
    {
        public Task<Card> CreateAsync(Card card);
        public Task<int> DeleteAsync(int id);
        public Task<Card> GetAsync(int id);
        public Task<Card> UpdateAsync(int id, Card card);
        public Card Create(Card card);
        public int Delete(int id);
        public Card Get(int id);
        public Card Update(int id, Card card);
    }
}
