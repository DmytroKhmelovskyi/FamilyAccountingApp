using FamilyAccounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyAccounting.Web.Interfaces
{
    public interface IWalletWebService
    {
        public Task<IEnumerable<WalletViewModel>> Get();
        public Task<WalletViewModel> Get(int id);
        public Task<WalletViewModel> Update(int id, WalletViewModel wallet);
        public Task<WalletViewModel> Create(WalletViewModel wallet);
        public Task<int> Delete(int id);
        public Task<IEnumerable<TransactionViewModel>> GetTransactions(int walletId);
        public Task<IEnumerable<TransactionViewModel>> GetTransactions(int walletId, DateTime from, DateTime to);
        public Task<WalletViewModel> MakeActive(int id);
    }
}
