using AutoMapper;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;
using FamilyAccounting.Web.Interfaces;
using FamilyAccounting.Web.Models;

namespace FamilyAccounting.Web.Services
{
    public class LoginWebService : ILoginWebService
    {
        private readonly IMapper mapper;
        private readonly IAuthenticationService authenticationService;

        public LoginWebService(IMapper mapper, IAuthenticationService authenticationService)
        {
            this.mapper = mapper;
            this.authenticationService = authenticationService;
        }
        public UserViewModel Login(string password, string login)
        {
            UserDTO user = authenticationService.Login(password, login);
            return mapper.Map<UserViewModel>(user);
        }
    }
}
