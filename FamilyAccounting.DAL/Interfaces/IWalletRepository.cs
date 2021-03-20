using FamilyAccounting.DAL.Entities;
using System.Collections.Generic;

namespace FamilyAccounting.DAL.Interfaces
{
   public interface IWalletRepository
    {
        IEnumerable<Wallet> Get();
        Wallet Get(int id);
    }
}
