using FamilyAccounting.BL.DTO;
using System.Threading.Tasks;

namespace FamilyAccounting.BL.Interfaces
{
    public interface ICardService
    {
        public Task<CardDTO> CreateAsync(CardDTO card);
        public Task<int> DeleteAsync(int id);
        public Task<CardDTO> GetAsync(int id);
        public Task<CardDTO> UpdateAsync(int id, CardDTO card);
        public CardDTO Create(CardDTO card);
        public int Delete(int id);
        public CardDTO Get(int id);
        public CardDTO Update(int id, CardDTO card);
    }
}
