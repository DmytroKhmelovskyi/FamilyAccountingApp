using FamilyAccounting.DAL.Entities;
using System.Collections.Generic;

namespace FamilyAccounting.DAL.Interfaces
{
   public interface IWalletsRepository
    {
        IEnumerable<Wallet> GetListOfWallets();
    }
}
