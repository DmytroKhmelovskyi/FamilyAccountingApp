using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System;
using System.Collections.Generic;

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

        public int Delete(int id)
        {
            try
            {
               return walletsRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<WalletDTO> Get()
        {
            try
            {
                IEnumerable<Wallet> wallet = walletsRepository.Get();
                return mapper.Map<IEnumerable<WalletDTO>>(wallet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public WalletDTO Get(int id)
        {
            Wallet wallet = walletsRepository.Get(id);
            return mapper.Map<WalletDTO>(wallet);
        }

        public WalletDTO Update(int id, WalletDTO wallet)
        {
            try
            {
                Wallet newWallet = mapper.Map<Wallet>(wallet);
                Wallet updatedWallet = walletsRepository.Update(id, newWallet);
                return mapper.Map<WalletDTO>(updatedWallet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
