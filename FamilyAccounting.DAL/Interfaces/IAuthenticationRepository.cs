using FamilyAccounting.DAL.Entities;

namespace FamilyAccounting.DAL.Interfaces
{
    public interface IAuthenticationRepository
    {
        public User Login(string password, string login);
    }
}
