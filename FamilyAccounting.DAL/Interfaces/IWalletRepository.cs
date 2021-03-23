using FamilyAccounting.DAL.Entities;
using System.Collections.Generic;

namespace FamilyAccounting.DAL.Interfaces
{
   public interface IWalletRepository
    {
        public IEnumerable<Wallet> Get();
        public Wallet Get(int id);
        public Wallet Update(int id, Wallet wallet);
        public int Delete(int id);
        public Wallet Create(Wallet wallet);
        public IEnumerable<Transaction> GetTransactions(int walletId);
    }
}
