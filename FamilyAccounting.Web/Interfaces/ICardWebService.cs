using FamilyAccounting.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyAccounting.Web.Interfaces
{
    public interface ICardWebService
    {
        public CardViewModel Create(CardViewModel card);
        public int Delete(int id);
        public CardViewModel Get(int id);
        public CardViewModel Update(int id, CardViewModel card);
    }
}
