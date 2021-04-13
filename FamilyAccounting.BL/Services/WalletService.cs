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
            Wallet _wallet = await walletsRepository.Create(mapper.Map<Wallet>(wallet));
            return mapper.Map<WalletDTO>(_wallet);
        }

        public async Task<int> Delete(int id)
        {
            return await walletsRepository.Delete(id);
        }

        public async Task<IEnumerable<WalletDTO>> Get()
        {
            IEnumerable<Wallet> wallet = await walletsRepository.Get();
            return mapper.Map<IEnumerable<WalletDTO>>(wallet);
        }

        public async Task<WalletDTO> Get(int id)
        {
            Wallet wallet = await walletsRepository.Get(id);
            return mapper.Map<WalletDTO>(wallet);
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactions(int walletId)
        {
            IEnumerable<Transaction> transactions = await walletsRepository.GetTransactions(walletId);
            return mapper.Map<IEnumerable<TransactionDTO>>(transactions);
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
            Wallet newWallet = mapper.Map<Wallet>(wallet);
            Wallet updatedWallet = await walletsRepository.Update(id, newWallet);
            return mapper.Map<WalletDTO>(updatedWallet);
        }
    }
}
