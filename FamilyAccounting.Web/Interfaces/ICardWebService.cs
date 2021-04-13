using FamilyAccounting.Web.Models;
using System.Threading.Tasks;

namespace FamilyAccounting.Web.Interfaces
{
    public interface ICardWebService
    {
        public  Task<CardViewModel> Create(CardViewModel card);
        public Task<int> Delete(int id);
        public Task<CardViewModel> Get(int id);
        public Task<CardViewModel> Update(int id, CardViewModel card);
    }
}
