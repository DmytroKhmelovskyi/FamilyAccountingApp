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
        public WalletService(IMapper mapper, IWalletRepository walletsRepository)
        {
            this.mapper = mapper;
            this.walletsRepository = walletsRepository;
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
    }
}
