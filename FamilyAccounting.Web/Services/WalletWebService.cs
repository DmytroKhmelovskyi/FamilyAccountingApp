using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<WalletViewModel> Create(WalletViewModel wallet)
        {
            try
            {
                WalletDTO _wallet = await walletService.CreateAsync(mapper.Map<WalletDTO>(wallet));
                return mapper.Map<WalletViewModel>(_wallet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                return await walletService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<WalletViewModel>> Get()
        {
            try
            {
                IEnumerable<WalletDTO> wallet = await walletService.GetAsync();
                return mapper.Map<IEnumerable<WalletViewModel>>(wallet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<WalletViewModel> Get(int id)
        {
            WalletDTO wallet = await walletService.GetAsync(id);
            return mapper.Map<WalletViewModel>(wallet);
        }

        public async Task<IEnumerable<TransactionViewModel>> GetTransactions(int walletId)
        {
            try
            {
                IEnumerable<TransactionDTO> transactions = await walletService.GetTransactionsAsync(walletId);
                return mapper.Map<IEnumerable<TransactionViewModel>>(transactions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<TransactionViewModel>> GetTransactions(int walletId, DateTime from, DateTime to)
        {
            IEnumerable<TransactionDTO> transactions = await walletService.GetTransactionsAsync(walletId, from, to);
            return mapper.Map<IEnumerable<TransactionViewModel>>(transactions);
        }

        public async Task<WalletViewModel> MakeActive(int id)
        {
            WalletDTO _wallet = await walletService.MakeActiveAsync(id);
            return mapper.Map<WalletViewModel>(_wallet);
        }

        public async Task<WalletViewModel> Update(int id, WalletViewModel wallet)
        {
            try
            {
                WalletDTO newWallet = mapper.Map<WalletDTO>(wallet);
                WalletDTO updatedWallet = await walletService.UpdateAsync(id, newWallet);
                return mapper.Map<WalletViewModel>(updatedWallet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
