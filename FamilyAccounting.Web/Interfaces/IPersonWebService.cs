using FamilyAccounting.Web.Models;
using System.Collections.Generic;

namespace FamilyAccounting.Web.Interfaces
{
    public interface IPersonWebService
    {
        public IEnumerable<PersonViewModel> Get();
        public IEnumerable<WalletViewModel> GetWallets(int id);
        public PersonViewModel Add(PersonViewModel person);
        public PersonViewModel Update(int id, PersonViewModel person);
        public PersonViewModel Get(int id);
        public int Delete(int id);
    }
}
