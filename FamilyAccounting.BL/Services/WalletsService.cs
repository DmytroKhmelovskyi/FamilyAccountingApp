using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace FamilyAccounting.BL.Services
{

    class WalletsService:IWalletsService
    {
        private readonly IMapper mapper;
        private IWalletsRepository walletsRepository;
        public WalletsService(IMapper mapper, IWalletsRepository walletsRepository)
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
