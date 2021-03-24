using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyAccounting.Web.Services
{
    public class WalletMapper
    {
        public static WalletViewModel WalletMap(WalletDTO walletDTO, IEnumerable<TransactionDTO> transactionDTO)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<WalletDTO, WalletViewModel>(); cfg.CreateMap<TransactionDTO, TransactionViewModel>(); });

            var mapper = new Mapper(config);
            var walletVM = mapper.Map<WalletViewModel>(walletDTO);
            walletVM.Transactions = mapper.Map<IEnumerable<TransactionViewModel>>(transactionDTO);
            return walletVM;
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

        public static WalletDTO WalletMap(WalletViewModel walletVM, PersonViewModel personVM)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<WalletViewModel, WalletDTO>(); cfg.CreateMap<PersonViewModel, PersonDTO>(); });
            var mapper = new Mapper(config);
            var walletDTO = mapper.Map<WalletDTO>(walletVM);
            //walletDTO.Person = mapper.Map<PersonDTO>(personVM);
            return walletDTO;
        }

        public static WalletViewModel WalletMap(WalletDTO walletDTO, PersonDTO personDTO)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<WalletDTO, WalletViewModel>(); cfg.CreateMap<PersonDTO, PersonViewModel>(); });
            var mapper = new Mapper(config);
            var walletVM = mapper.Map<WalletViewModel>(walletDTO);
            //walletVM.Person = mapper.Map<PersonViewModel>(personDTO);
            return walletVM;
        }
    }
}
