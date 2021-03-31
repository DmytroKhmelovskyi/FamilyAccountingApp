using FamilyAccounting.Web.Models;

namespace FamilyAccounting.Web.Interfaces
{
    public interface ILoginWebService
    {
        public UserViewModel Login(string password, string login);
    }
}
