using FamilyAccounting.BL.DTO;

namespace FamilyAccounting.BL.Interfaces
{
    public interface IAuthenticationService
    {
        public UserDTO Login(string password, string login);
    }
}
