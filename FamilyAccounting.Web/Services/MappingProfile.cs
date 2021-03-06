using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.Web.Models;

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
            CreateMap<RoleDTO, RoleViewModel>();
            CreateMap<RoleViewModel, RoleDTO>();
            CreateMap<UserDTO, UserViewModel>();
            CreateMap<UserViewModel, UserDTO>();
            CreateMap<AuditActionViewModel, AuditActionDTO>();
            CreateMap<AuditActionDTO, AuditActionViewModel>();
            CreateMap<AuditWalletViewModel, AuditWalletDTO>();
            CreateMap<AuditWalletDTO, AuditWalletViewModel>();
            CreateMap<AuditPersonViewModel, AuditPersonDTO>();
            CreateMap<AuditPersonDTO, AuditPersonViewModel>();
        }
    }
}
