using FamilyAccounting.BL.DTO;
using System.Collections.Generic;

namespace FamilyAccounting.BL.Interfaces
{
    public interface IWalletService
    {
        IEnumerable<WalletDTO> Get();
        WalletDTO Get(int id);
        WalletDTO Update(int id, WalletDTO wallet);
        public void Delete(int id);
        public WalletDTO Create(WalletDTO wallet);
    }
}
