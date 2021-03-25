using FamilyAccounting.Web.Models;

namespace FamilyAccounting.Web.Interfaces
{
    public interface ICardWebService
    {
        public CardViewModel Create(CardViewModel card);
        public int Delete(int id);
        public CardViewModel Get(int id);
        public CardViewModel Update(int id, CardViewModel card);
    }
}
