using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using System;
using System.Collections.Generic;

namespace FamilyAccounting.Web.Services
{
    public class WalletWebService : IWalletWebService
    {
        private readonly IMapper mapper;
        private IWalletService walletService;
        public WalletWebService(IMapper mapper, IWalletService walletService)
        {
            this.mapper = mapper;
            this.walletService = walletService;
        }

        public WalletViewModel Create(WalletViewModel wallet)
        {
            try
            {
                WalletDTO _wallet = walletService.Create(mapper.Map<WalletDTO>(wallet));
                return mapper.Map<WalletViewModel>(_wallet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Delete(int id)
        {
            try
            {
                return walletService.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<WalletViewModel> Get()
        {
            try
            {
                IEnumerable<WalletDTO> wallet = walletService.Get();
                return mapper.Map<IEnumerable<WalletViewModel>>(wallet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public WalletViewModel Get(int id)
        {
            WalletDTO wallet = walletService.Get(id);
            return mapper.Map<WalletViewModel>(wallet);
        }

        public IEnumerable<TransactionViewModel> GetTransactions(int walletId)
        {
            try
            {
                IEnumerable<TransactionDTO> transactions = walletService.GetTransactions(walletId);
                return mapper.Map<IEnumerable<TransactionViewModel>>(transactions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<TransactionViewModel> GetTransactions(int walletId, DateTime from, DateTime to)
        {
            IEnumerable<TransactionDTO> transactions = walletService.GetTransactions(walletId, from, to);
            return mapper.Map<IEnumerable<TransactionViewModel>>(transactions);
        }

        public WalletViewModel MakeActive(int id)
        {
            WalletDTO _wallet = walletService.MakeActive(id);
            return mapper.Map<WalletViewModel>(_wallet);
        }

        public WalletViewModel Update(int id, WalletViewModel wallet)
        {
            try
            {
                WalletDTO newWallet = mapper.Map<WalletDTO>(wallet);
                WalletDTO updatedWallet = walletService.Update(id, newWallet);
                return mapper.Map<WalletViewModel>(updatedWallet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
