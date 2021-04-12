using FamilyAccounting.DAL.Entities;
using System.Threading.Tasks;

namespace FamilyAccounting.DAL.Interfaces
{
    public interface ICardRepository
    {
        public Task<Card> Create(Card card);
        public Task<int> Delete(int id);
        public Task<Card> Get(int id);
        public Task<Card> Update(int id, Card card);
    }
}
