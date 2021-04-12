using FamilyAccounting.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyAccounting.DAL.Interfaces
{
    public interface IWalletRepository
    {
        public Task<IEnumerable<Wallet>> Get();
        public Task<Wallet> Get(int id);
        public Task<Wallet> Update(int id, Wallet wallet);
        public Task<int> Delete(int id);
        public Task<Wallet> Create(Wallet wallet);
        public Task<IEnumerable<Transaction>> GetTransactions(int walletId);
        public Task<IEnumerable<Transaction>> GetTransactions(int walletId, DateTime from, DateTime to);
        public Task<Wallet> MakeActive(int id);
    }
}
