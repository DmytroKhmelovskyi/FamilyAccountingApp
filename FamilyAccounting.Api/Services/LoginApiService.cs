using FamilyAccounting.Api.Interfaces;
using FamilyAccounting.BL.DTO;
using FamilyAccounting.BL.Interfaces;

namespace FamilyAccounting.Api.Services
{
    public class LoginApiService : ILoginApiService
    {
        private readonly IAuthenticationService authenticationService;

        public LoginApiService(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }
        public UserDTO Login(string password, string login)
        {
            UserDTO user = authenticationService.Login(password, login);
            return user;
        }

    }
}
