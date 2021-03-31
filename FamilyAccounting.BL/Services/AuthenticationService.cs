using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.DAL.Entities;
using FamilyAccounting.DAL.Interfaces;

namespace FamilyAccounting.BL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper mapper;
        private IAuthenticationRepository authenticationRepository;

        public AuthenticationService(IMapper mapper, IAuthenticationRepository authenticationRepository)
        {
            this.mapper = mapper;
            this.authenticationRepository = authenticationRepository;
        }
        public UserDTO Login(string password, string login)
        {
            User user = authenticationRepository.Login(password, login);
            return mapper.Map<UserDTO>(user);
        }
    }
}
