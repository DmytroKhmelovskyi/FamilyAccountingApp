﻿using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.Web.Models;
using System.Collections.Generic;

namespace FamilyAccounting.Web.Services
{
    public class MapperService
    {
        public static IndexPersonViewModel PersonMap(IEnumerable<PersonDTO> personDTO)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PersonDTO, PersonViewModel>());
            var mapper = new Mapper(config);
            var personVM = mapper.Map<IEnumerable<PersonViewModel>>(personDTO);
            var indexVM = new IndexPersonViewModel
            {
                Persons = personVM
            };
            return indexVM;
        }
        public static PersonViewModel PersonMap(PersonDTO personDTO)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PersonDTO, PersonViewModel>());
            var mapper = new Mapper(config);
            var personVM = mapper.Map<PersonViewModel>(personDTO);
            return personVM;
        }
        public static PersonViewModel PersonMap(PersonDTO personDTO, IEnumerable<WalletDTO> walletDTO)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<PersonDTO, PersonViewModel>(); cfg.CreateMap<WalletDTO, WalletViewModel>(); });

            var mapper = new Mapper(config);
            var personVM = mapper.Map<PersonViewModel>(personDTO);
            personVM.Wallets = mapper.Map<IEnumerable<WalletViewModel>>(walletDTO);
            return personVM;
        }
        public static PersonDTO PersonMap(PersonViewModel personVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PersonViewModel, PersonDTO>());
            var mapper = new Mapper(config);
            var personDTO = mapper.Map<PersonDTO>(personVM);
            return personDTO;
        }
        public static WalletDTO WalletMap(WalletViewModel walletVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<WalletViewModel, WalletDTO>());
            var mapper = new Mapper(config);
            var walletDTO = mapper.Map<WalletDTO>(walletVM);
            return walletDTO;
        }
        public static WalletViewModel WalletMap(WalletDTO walletVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<WalletDTO, WalletViewModel>());
            var mapper = new Mapper(config);
            var walletDTO = mapper.Map<WalletViewModel>(walletVM);
            return walletDTO;
        }
        public static IndexWalletViewModel WalletMap(IEnumerable<WalletDTO> walletDTO)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<WalletDTO, WalletViewModel>());
            var mapper = new Mapper(config);
            var walletVM = mapper.Map<IEnumerable<WalletViewModel>>(walletDTO);
            var indexVM = new IndexWalletViewModel
            {
                Wallets = walletVM
            };
            return indexVM;
        }

    }
}
