using FamilyAccounting.BL.DTO;
using System.Threading.Tasks;

namespace FamilyAccounting.BL.Interfaces
{
    public interface ICardService
    {
        public Task<CardDTO> Create(CardDTO card);
        public Task<int> Delete(int id);
        public Task<CardDTO> Get(int id);
        public Task<CardDTO> Update(int id, CardDTO card);
    }
}
