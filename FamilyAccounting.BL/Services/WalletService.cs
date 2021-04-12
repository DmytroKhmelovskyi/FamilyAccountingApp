using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyAccounting.BL.Services
{

    public class WalletService : IWalletService
    {
        private readonly IMapper mapper;
        private IWalletRepository walletsRepository;
        public WalletService(IWalletRepository walletsRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.walletsRepository = walletsRepository;
        }

        public async Task<WalletDTO> Create(WalletDTO wallet)
        {
            try
            {
                Wallet _wallet = await walletsRepository.Create(mapper.Map<Wallet>(wallet));
                return mapper.Map<WalletDTO>(_wallet);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Delete(int id)
        {
            try
            {
               return await walletsRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<WalletDTO>> Get()
        {
            try
            {
                IEnumerable<Wallet> wallet = await walletsRepository.Get();
                return mapper.Map<IEnumerable<WalletDTO>>(wallet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<WalletDTO> Get(int id)
        {
            Wallet wallet = await walletsRepository.Get(id);
            return mapper.Map<WalletDTO>(wallet);
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactions(int walletId)
        {
            try
            {
                IEnumerable<Transaction> transactions = await walletsRepository.GetTransactions(walletId);
                return mapper.Map<IEnumerable<TransactionDTO>>(transactions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactions(int walletId, DateTime from, DateTime to)
        {
            IEnumerable<Transaction> transactions = await walletsRepository.GetTransactions(walletId, from, to);
            return mapper.Map<IEnumerable<TransactionDTO>>(transactions);
        }

        public async Task<WalletDTO> MakeActive(int id)
        {
            Wallet wallet = await walletsRepository.MakeActive(id);
            return mapper.Map<WalletDTO>(wallet);
        }

        public async Task<WalletDTO> Update(int id, WalletDTO wallet)
        {
            try
            {
                Wallet newWallet = mapper.Map<Wallet>(wallet);
                Wallet updatedWallet = await walletsRepository.Update(id, newWallet);
                return mapper.Map<WalletDTO>(updatedWallet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
