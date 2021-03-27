using FamilyAccounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyAccounting.Web.Interfaces
{
    public interface ILoginWebService
    {
        public UserViewModel Login(string password, string login);
    }
}
