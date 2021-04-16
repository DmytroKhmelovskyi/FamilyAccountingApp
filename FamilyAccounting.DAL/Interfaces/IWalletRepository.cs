using FamilyAccounting.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyAccounting.DAL.Interfaces
{
    public interface IWalletRepository
    {
        public Task<IEnumerable<Wallet>> GetAsync();
        public Task<Wallet> GetAsync(int id);
        public Task<Wallet> UpdateAsync(int id, Wallet wallet);
        public Task<int> DeleteAsync(int id);
        public Task<Wallet> CreateAsync(Wallet wallet);
        public Task<IEnumerable<Transaction>> GetTransactionsAsync(int walletId);
        public Task<IEnumerable<Transaction>> GetTransactionsAsync(int walletId, DateTime from, DateTime to);
        public Task<Wallet> MakeActiveAsync(int id);
        public IEnumerable<Wallet> Get();
        public Wallet Get(int id);
        public Wallet Update(int id, Wallet wallet);
        public int Delete(int id);
        public Wallet Create(Wallet wallet);
        public IEnumerable<Transaction> GetTransactions(int walletId);
        public IEnumerable<Transaction> GetTransactions(int walletId, DateTime from, DateTime to);
        public Wallet MakeActive(int id);
    }
}
