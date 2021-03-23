using FamilyAccounting.DAL.Entities;

namespace FamilyAccounting.DAL.Interfaces
{
    public interface ICardRepository
    {
        public Card Create(Card card);
        public int Delete(int id);
        public Card Get(int id);
        public Card Update(int id, Card card);
    }
}
