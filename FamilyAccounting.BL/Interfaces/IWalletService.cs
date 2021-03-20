using FamilyAccounting.BL.DTO;
using System.Collections.Generic;

namespace FamilyAccounting.BL.Interfaces
{
    public interface IWalletService
    {
        IEnumerable<WalletDTO> Get();
        WalletDTO Get(int id);
    }
}
