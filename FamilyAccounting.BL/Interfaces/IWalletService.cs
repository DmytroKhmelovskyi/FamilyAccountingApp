using FamilyAccounting.BL.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyAccounting.BL.Interfaces
{
    public interface IWalletService
    {
        public Task<IEnumerable<WalletDTO>> Get();
        public Task<WalletDTO> Get(int id);
        public Task<WalletDTO> Update(int id, WalletDTO wallet);
        public Task<WalletDTO> Create(WalletDTO wallet);
        public Task<int> Delete(int id);
        public Task<IEnumerable<TransactionDTO>> GetTransactions(int walletId);
        public Task<IEnumerable<TransactionDTO>> GetTransactions(int walletId, DateTime from, DateTime to);
        public Task<WalletDTO> MakeActive(int id);
    }
}
