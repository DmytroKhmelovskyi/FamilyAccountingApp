using FamilyAccounting.DAL.Entities;
using System.Collections.Generic;

namespace FamilyAccounting.DAL.Interfaces
{
   public interface IWalletRepository
    {
        IEnumerable<Wallet> Get();
        Wallet Get(int id);
        Wallet Update(int id, Wallet wallet);
        public int Delete(int id);
        public Wallet Create(Wallet wallet);
    }
}
