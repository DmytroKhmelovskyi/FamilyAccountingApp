﻿using FamilyAccounting.Web.Models;
using System.Collections.Generic;

namespace FamilyAccounting.Web.Interfaces
{
    public interface IWalletWebService
    {
        public IEnumerable<WalletViewModel> Get();
        public WalletViewModel Get(int id);
        public WalletViewModel Update(int id, WalletViewModel wallet);
        public WalletViewModel Create(WalletViewModel wallet);
        public int Delete(int id);
        public IEnumerable<TransactionViewModel> GetTransactions(int walletId);
    }
}