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
    public class TransactionMapper
    {
        public static IEnumerable<TransactionViewModel> TransactionMap(IEnumerable<TransactionDTO> transactionDTO)

        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TransactionDTO, TransactionViewModel>());
            var mapper = new Mapper(config);
            var transactionVM = mapper.Map<IEnumerable<TransactionViewModel>>(transactionDTO);
            return transactionVM;
        }


        public static TransactionDTO TransactionMap(TransactionViewModel transactionVM, WalletViewModel walletVM, CategoryViewModel categoryVM)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<TransactionViewModel, TransactionDTO>(); cfg.CreateMap<WalletViewModel, WalletDTO>(); cfg.CreateMap<CategoryViewModel, CategoryDTO>(); });
            var mapper = new Mapper(config);
            var transactionDTO = mapper.Map<TransactionDTO>(transactionVM);
            //transactionDTO.Category = mapper.Map<CategoryDTO>(categoryVM);
            //transactionDTO.SourceWallet = mapper.Map<WalletDTO>(walletVM);
            return transactionDTO;
        }

        public static TransactionViewModel TransactionMap(TransactionDTO transactionDTO)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TransactionDTO, TransactionViewModel>());
            var mapper = new Mapper(config);
            var transactionVM = mapper.Map<TransactionViewModel>(transactionDTO);
            return transactionVM;
        }

        public static TransactionDTO TransactionMap(TransactionViewModel transactionVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TransactionViewModel, TransactionDTO>());
            var mapper = new Mapper(config);
            var transactionDTO = mapper.Map<TransactionDTO>(transactionVM);
            return transactionDTO;
        }
    }
}
