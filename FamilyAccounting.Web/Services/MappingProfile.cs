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
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<PersonViewModel, PersonDTO>();
            CreateMap<PersonDTO, PersonViewModel>();
            CreateMap<CategoryViewModel, CategoryDTO>();
            CreateMap<CategoryDTO, CategoryViewModel>();
            CreateMap<CardViewModel, CardDTO>();
            CreateMap<CardDTO, CardViewModel>();
            CreateMap<TransactionViewModel, TransactionDTO>();
            CreateMap<TransactionDTO, TransactionViewModel>();
            CreateMap<WalletViewModel, WalletDTO>();
            CreateMap<WalletDTO, WalletViewModel>();
        }
    }
}
