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

        public async Task<WalletDTO> CreateAsync(WalletDTO wallet)
        {
            Wallet _wallet = await walletsRepository.CreateAsync(mapper.Map<Wallet>(wallet));
            return mapper.Map<WalletDTO>(_wallet);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await walletsRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<WalletDTO>> GetAsync()
        {
            IEnumerable<Wallet> wallet = await walletsRepository.GetAsync();
            return mapper.Map<IEnumerable<WalletDTO>>(wallet);
        }

        public async Task<WalletDTO> GetAsync(int id)
        {
            Wallet wallet = await walletsRepository.GetAsync(id);
            return mapper.Map<WalletDTO>(wallet);
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsAsync(int walletId)
        {
            IEnumerable<Transaction> transactions = await walletsRepository.GetTransactionsAsync(walletId);
            return mapper.Map<IEnumerable<TransactionDTO>>(transactions);
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsAsync(int walletId, DateTime from, DateTime to)
        {
            IEnumerable<Transaction> transactions = await walletsRepository.GetTransactionsAsync(walletId, from, to);
            return mapper.Map<IEnumerable<TransactionDTO>>(transactions);
        }

        public async Task<WalletDTO> MakeActiveAsync(int id)
        {
            Wallet wallet = await walletsRepository.MakeActiveAsync(id);
            return mapper.Map<WalletDTO>(wallet);
        }

        public async Task<WalletDTO> UpdateAsync(int id, WalletDTO wallet)
        {
            Wallet newWallet = mapper.Map<Wallet>(wallet);
            Wallet updatedWallet = await walletsRepository.UpdateAsync(id, newWallet);
            return mapper.Map<WalletDTO>(updatedWallet);
        }

        public WalletDTO Create(WalletDTO wallet)
        {
            Wallet _wallet = walletsRepository.Create(mapper.Map<Wallet>(wallet));
            return mapper.Map<WalletDTO>(_wallet);
        }

        public int Delete(int id)
        {
            return walletsRepository.Delete(id);
        }

        public IEnumerable<WalletDTO> Get()
        {
            IEnumerable<Wallet> wallet = walletsRepository.Get();
            return mapper.Map<IEnumerable<WalletDTO>>(wallet);
        }

        public WalletDTO Get(int id)
        {
            Wallet wallet = walletsRepository.Get(id);
            return mapper.Map<WalletDTO>(wallet);
        }

        public IEnumerable<TransactionDTO> GetTransactions(int walletId)
        {
            IEnumerable<Transaction> transactions = walletsRepository.GetTransactions(walletId);
            return mapper.Map<IEnumerable<TransactionDTO>>(transactions);
        }

        public IEnumerable<TransactionDTO> GetTransactions(int walletId, DateTime from, DateTime to)
        {
            IEnumerable<Transaction> transactions = walletsRepository.GetTransactions(walletId, from, to);
            return mapper.Map<IEnumerable<TransactionDTO>>(transactions);
        }

        public WalletDTO MakeActive(int id)
        {
            Wallet wallet = walletsRepository.MakeActive(id);
            return mapper.Map<WalletDTO>(wallet);
        }

        public WalletDTO Update(int id, WalletDTO wallet)
        {
            Wallet newWallet = mapper.Map<Wallet>(wallet);
            Wallet updatedWallet = walletsRepository.Update(id, newWallet);
            return mapper.Map<WalletDTO>(updatedWallet);
        }
    }
}
