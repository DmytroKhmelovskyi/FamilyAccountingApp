using FamilyAccounting.BL.DTO;

namespace FamilyAccounting.Api.Interfaces
{
    public interface ILoginApiService
    {
        public UserDTO Login(string password, string login);
    }
}
