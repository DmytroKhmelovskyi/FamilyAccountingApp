using FamilyAccounting.BL.DTO;

namespace FamilyAccounting.BL.Interfaces
{
    public interface ICardService
    {
        public CardDTO Create(CardDTO card);
        public int Delete(int id);
        public CardDTO Get(int id);
        public CardDTO Update(int id, CardDTO card);
    }
}
