using FamilyAccounting.BL.DTO;
using System.Collections.Generic;

namespace FamilyAccounting.BL.Interfaces
{
    public interface IWalletService
    {
        public IEnumerable<WalletDTO> Get();
        public WalletDTO Get(int id);
        public WalletDTO Update(int id, WalletDTO wallet);
        public WalletDTO Create(WalletDTO wallet);
        public int Delete(int id);
        public IEnumerable<TransactionDTO> GetTransactions(int walletId);
    }
}
