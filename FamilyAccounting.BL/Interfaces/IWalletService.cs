using FamilyAccounting.BL.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyAccounting.BL.Interfaces
{
    public interface IWalletService
    {
        public Task<IEnumerable<WalletDTO>> GetAsync();
        public Task<WalletDTO> GetAsync(int id);
        public Task<WalletDTO> UpdateAsync(int id, WalletDTO wallet);
        public Task<WalletDTO> CreateAsync(WalletDTO wallet);
        public Task<int> DeleteAsync(int id);
        public Task<IEnumerable<TransactionDTO>> GetTransactionsAsync(int walletId);
        public Task<IEnumerable<TransactionDTO>> GetTransactionsAsync(int walletId, DateTime from, DateTime to);
        public Task<WalletDTO> MakeActiveAsync(int id);
        public IEnumerable<WalletDTO> Get();
        public WalletDTO Get(int id);
        public WalletDTO Update(int id, WalletDTO wallet);
        public WalletDTO Create(WalletDTO wallet);
        public int Delete(int id);
        public IEnumerable<TransactionDTO> GetTransactions(int walletId);
        public IEnumerable<TransactionDTO> GetTransactions(int walletId, DateTime from, DateTime to);
        public WalletDTO MakeActive(int id);
    }
}
