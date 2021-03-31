using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.DAL.Entities;

namespace FamilyAccounting.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDTO>();
            CreateMap<PersonDTO, Person>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<Card, CardDTO>();
            CreateMap<CardDTO, Card>();
            CreateMap<Transaction, TransactionDTO>();
            CreateMap<TransactionDTO, Transaction>();
            CreateMap<Wallet, WalletDTO>();
            CreateMap<WalletDTO, Wallet>();
            CreateMap<RoleDTO, Role>();
            CreateMap<Role, RoleDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
            CreateMap<AuditAction, AuditActionDTO>();
            CreateMap<AuditActionDTO, AuditAction>();
            CreateMap<AuditWallet, AuditWalletDTO>();
            CreateMap<AuditWalletDTO, AuditWallet>();
            CreateMap<AuditPerson, AuditPersonDTO>();
            CreateMap<AuditPersonDTO, AuditPerson>();
        }
    }
}
