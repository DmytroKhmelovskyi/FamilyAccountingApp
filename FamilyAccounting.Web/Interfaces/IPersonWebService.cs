using FamilyAccounting.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyAccounting.Web.Interfaces
{
    public interface IPersonWebService
    {
        public Task<IEnumerable<PersonViewModel>> Get();
        public Task<IEnumerable<WalletViewModel>>GetWallets(int id);
        public Task<PersonViewModel> Add(PersonViewModel person);
        public Task<PersonViewModel> Update(int id, PersonViewModel person);
        public Task<PersonViewModel> Get(int id);
        public Task<int> Delete(int id);
    }
}
